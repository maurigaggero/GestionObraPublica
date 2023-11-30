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
    [Route("GOP/api/itemscontrolsparams")]
    public class ItemsControlsParamsController : GOPBaseController
    {
        private readonly IItemsControlsParamsRepositorio repositorio;
        private readonly IMapper mapper;

        public ItemsControlsParamsController(IItemsControlsParamsRepositorio repositorio, 
                                             IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ItemControlParamDTO>>> GetFull()
        {
            try
            {
                List<ItemControlParam> lista = await repositorio.GetActivos();

                List<ItemControlParamDTO> result = mapper.Map<List<ItemControlParamDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getitemcontrolparams")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ItemControlParamDTO>>> GetItemControlParams() //CON INCLUDE
        {
            try
            {
                List<ItemControlParam> lista = await repositorio.GetItemControlParams();

                List<ItemControlParamDTO> result = mapper.Map<List<ItemControlParamDTO>>(lista);
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
        public async Task<ActionResult<ItemControlParamDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetItemControlParamById(id);
                if (res == null) return NotFound();

                ItemControlParamDTO result = mapper.Map<ItemControlParamDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpGet("getactivos")]
        //public async Task<ActionResult<List<ItemControlParamDTO>>> GetActivos()
        //{
        //    try
        //    {
        //        List<ItemControlParam> lista = await repositorio.GetActivos();

        //        List<ItemControlParamDTO> result = mapper.Map<List<ItemControlParamDTO>>(lista);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos")]
        public async Task<ActionResult<int>> Post(ItemControlParamDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                ItemControlParam entidad = mapper.Map<ItemControlParam>(dto);

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<ItemControlParam>(entidad);
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
        public async Task<ActionResult<bool>> Put(ItemControlParamDTO dto, int id)
        {
            try
            {
                dto.ItemControl = null;
                ItemControlParam entidad = mapper.Map<ItemControlParam>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<ItemControlParam>(entidad);
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
