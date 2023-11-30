using AutoMapper;
using GOP.BD.Data;
using GOP.BD.Data.Entity;
using GOP.Repositorio.Repos;
using GOP.Shared.DTOs.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GOP.Server.Controllers.GOPControllers
{
    [ApiController]
    [Route("GOP/api/itemscontrols")]
    public class ItemsControlsController : GOPBaseController
    {
        private readonly IItemsControlsRepositorio repositorio;
        private readonly IMapper mapper;

        public ItemsControlsController(IItemsControlsRepositorio repositorio, 
                                       IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ItemControlDTO>>> GetFull()
        {
            try
            {
                List<ItemControl> lista = await repositorio.GetActivos();

                List<ItemControlDTO> result = mapper.Map<List<ItemControlDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getitemcontrols")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ItemControlDTO>>> GetItemControls() //CON INCLUDE
        {
            try
            {
                List<ItemControl> lista = await repositorio.GetItemControls();

                List<ItemControlDTO> result = mapper.Map<List<ItemControlDTO>>(lista);
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
        public async Task<ActionResult<ItemControlDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetItemControlById(id);
                if (res == null) return NotFound();

                ItemControlDTO result = mapper.Map<ItemControlDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("get/{idItem:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ItemControlDTO>>> GetItemControlsByItem(int idItem) //CON INCLUDE
        {
            try
            {
                List<ItemControl> lista = await repositorio.GetItemControlsByItem(idItem);

                List<ItemControlDTO> result = mapper.Map<List<ItemControlDTO>>(lista);
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
        public async Task<ActionResult<int>> Post(ItemControlDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                ItemControl entidad = mapper.Map<ItemControl>(dto);

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<ItemControl>(entidad);
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
        public async Task<ActionResult<bool>> Put(ItemControlDTO dto, int id)
        {
            try
            {
                dto.Parametros = null;
                dto.Item = null;
                dto.Documentos = null;
                ItemControl entidad = mapper.Map<ItemControl>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<ItemControl>(entidad);
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
