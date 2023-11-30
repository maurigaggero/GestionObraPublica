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
    [Route("GOP/api/ContratoItemControlParams")]
    public class ContratoItemControlParamsController : GOPBaseController
    {
        private readonly IContratoItemControlParamsRepositorio repositorio;
        private readonly IMapper mapper;

        public ContratoItemControlParamsController(IContratoItemControlParamsRepositorio repositorio, 
                                                   IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoItemControlParamDTO>>> GetFull()
        {
            try
            {
                List<ContratoItemControlParam> lista = await repositorio.GetActivos();

                List<ContratoItemControlParamDTO> result = mapper.Map<List<ContratoItemControlParamDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getContratoItemControlParam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoItemControlParamDTO>>> GetContratoItemControlParam() //CON INCLUDE
        {
            try
            {
                List<ContratoItemControlParam> lista = await repositorio.GetContratoItemControlParams();

                List<ContratoItemControlParamDTO> result = mapper.Map<List<ContratoItemControlParamDTO>>(lista);
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
        public async Task<ActionResult<ContratoItemControlParamDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetContratoItemControlParamById(id);
                if (res == null) return NotFound();

                ContratoItemControlParamDTO result = mapper.Map<ContratoItemControlParamDTO>(res);
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
        public async Task<ActionResult<int>> Post(ContratoItemControlParamDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                ContratoItemControlParam entidad = mapper.Map<ContratoItemControlParam>(dto);

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<ContratoItemControlParam>(entidad);
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
        public async Task<ActionResult<bool>> Put(ContratoItemControlParamDTO dto, int id)
        {
            try
            {
                dto.ContratoItemControl = null;
                dto.Unidad = null;
                ContratoItemControlParam entidad = mapper.Map<ContratoItemControlParam>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<ContratoItemControlParam>(entidad);
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
                    return Ok($"El ContratoItemControlParam ha sido dado de baja del sistema");
                }
                return NotFound("El ContratoItemControlParam a dar de baja no a sido encontrado");
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
        //            return Ok($"El ContratoItemControlParam ha sido borrado");
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
