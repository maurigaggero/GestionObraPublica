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
    [Route("GOP/api/ItemDoc")]

    public class ItemDocController : GOPBaseController
    {
        private readonly IItemDocRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;

        public ItemDocController(IItemDocRepositorio repositorio, 
                                 IMapper mapper, 
                                 IAlmacenadorArchivos almacenadorArchivos)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet("getfull")]
        public async Task<ActionResult<List<ItemDocDTO>>> GetFull()
        {
            try
            {
                List<ItemDoc> lista = await repositorio.GetActivos();

                List<ItemDocDTO> result = mapper.Map<List<ItemDocDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getItemDoc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ItemDocDTO>>> GetItemDoc() //CON INCLUDE
        {
            try
            {
                List<ItemDoc> lista = await repositorio.GetItemDocs();

                List<ItemDocDTO> result = mapper.Map<List<ItemDocDTO>>(lista);
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
        public async Task<ActionResult<ItemDocDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetItemDocById(id);
                if (res == null) return NotFound();

                ItemDocDTO result = mapper.Map<ItemDocDTO>(res);
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
        public async Task<ActionResult<int>> Post(ItemDocDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                ItemDoc entidad = mapper.Map<ItemDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.GuardarArchivo(archivo, dto.Extension, "Items");
                }

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<ItemDoc>(entidad);
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
        public async Task<ActionResult<bool>> Put(ItemDocDTO dto, int id)
        {
            try
            {
                dto.Item = null;
                ItemDoc entidad = mapper.Map<ItemDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.GuardarArchivo(archivo, dto.Extension, "Items");
                }

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<ItemDoc>(entidad);
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
                //ItemDocDTO doc = Get(id).Result.Value;
                //await almacenadorArchivos.EliminarArchivo(doc.PathDoc);

                if (await repositorio.Baja(id, ObtenerUserId()))
                {
                    return Ok($"El ItemDoc ha sido dado de baja del sistema");
                }
                return NotFound("El ItemDoc a dar de baja no a sido encontrado");
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
        //        ItemDocDTO doc = Get(id).Result.Value;
        //        await almacenadorArchivos.EliminarArchivo(doc.PathDoc);

        //        if (await repositorio.Delete(id))
        //        {
        //            return Ok($"El ItemDoc ha sido borrado");
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
