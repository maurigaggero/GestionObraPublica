using AutoMapper;
using GOP.BD.Data;
using GOP.BD.Data.Entity;
using GOP.Repositorio.Repos;
using GOP.Server.Helpers;
using GOP.Shared.DTOs.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GOP.Server.Controllers.GOPControllers
{
    [ApiController]
    [Route("GOP/api/itemscontrolsdocs")]
    public class ItemsControlDocsController : GOPBaseController
    {
        private readonly IItemsControlsDocsRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;

        public ItemsControlDocsController(IItemsControlsDocsRepositorio repositorio, 
                                          IMapper mapper, 
                                          IAlmacenadorArchivos almacenadorArchivos)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ItemControlDocDTO>>> GetFull()
        {
            try
            {
                List<ItemControlDoc> lista = await repositorio.GetActivos();
                List<ItemControlDocDTO> result = mapper.Map<List<ItemControlDocDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getitemscontroldocs")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ItemControlDocDTO>>> GetItemsControls() //CON INCLUDE
        {
            try
            {
                List<ItemControlDoc> lista = await repositorio.GetItemControls();
                List<ItemControlDocDTO> result = mapper.Map<List<ItemControlDocDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<ItemControlDocDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetItemControlById(id);
                if (res == null) return NotFound();

                ItemControlDocDTO result = mapper.Map<ItemControlDocDTO>(res);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos")]
        public async Task<ActionResult<int>> Post(ItemControlDocDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");
                ItemControlDoc entidad = mapper.Map<ItemControlDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.GuardarArchivo(archivo, dto.Extension, dto.Carpeta);
                }

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<ItemControlDoc>(entidad);
                #endregion

                var resp = await repositorio.Insert(entidad);
                if (resp > 0) return Ok(resp);
                else return BadRequest("No se pudo agregar el tipo de estado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos")]
        public async Task<ActionResult<bool>> Put(ItemControlDocDTO dto, int id)
        {
            try
            {
                dto.ItemControl = null;
                ItemControlDoc entidad = mapper.Map<ItemControlDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.EditarArchivo(archivo, dto.Extension, dto.Carpeta, entidad.PathDoc);
                }

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<ItemControlDoc>(entidad);
                #endregion

                var resp = await repositorio.Update(entidad, id);
                if (resp)
                {
                    return Ok(resp);
                }
                else
                {
                    return BadRequest("Datos incorrectos");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("baja/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos")]
        public async Task<ActionResult> Baja(int id)
        {
            try
            {
                //ItemControlDocDTO item = Get(id).Result.Value;
                //await almacenadorArchivos.EliminarArchivo(item.PathDoc);
                if (await repositorio.Baja(id, ObtenerUserId()))
                {
                    return Ok($"El Item ha sido dada de baja del sistema");
                }
                return NotFound("El Item a dar de baja no a sido encontrado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpDelete("{id:int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        //Roles = "Admin, BaseDatos")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        ItemControlDocDTO item = Get(id).Result.Value;
        //        await almacenadorArchivos.EliminarArchivo(item.PathDoc);
        //        if (await repositorio.Delete(id))
        //        {
        //            return Ok($"El Item ha sido borrado");
        //        }
        //        return NotFound("El Tipo de Estado que desea borrar no a sido encontrado");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}

