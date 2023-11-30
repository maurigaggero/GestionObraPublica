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
    [Route("GOP/api/CertificadoItemControlParams")]
    public class CertificadoItemControlParamsController : GOPBaseController
    {
        private readonly ICertificadoItemControlParamsRepositorio repositorio;
        private readonly IMapper mapper;

        public CertificadoItemControlParamsController(ICertificadoItemControlParamsRepositorio repositorio, 
                                                      IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoItemControlParamDTO>>> GetFull()
        {
            try
            {
                List<CertificadoItemControlParam> lista = await repositorio.GetActivos();

                List<CertificadoItemControlParamDTO> result = mapper.Map<List<CertificadoItemControlParamDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getCertificadoItemControlParam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoItemControlParamDTO>>> GetCertificadoItemControlParam() //CON INCLUDE
        {
            try
            {
                List<CertificadoItemControlParam> lista = await repositorio.GetCertificadoItemControlParams();

                List<CertificadoItemControlParamDTO> result = mapper.Map<List<CertificadoItemControlParamDTO>>(lista);
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
        public async Task<ActionResult<CertificadoItemControlParamDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetCertificadoItemControlParamById(id);
                if (res == null) return NotFound();

                CertificadoItemControlParamDTO result = mapper.Map<CertificadoItemControlParamDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente")]
        public async Task<ActionResult<int>> Post(CertificadoItemControlParamDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                CertificadoItemControlParam entidad = mapper.Map<CertificadoItemControlParam>(dto);

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<CertificadoItemControlParam>(entidad);
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
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente")]
        public async Task<ActionResult<bool>> Put(CertificadoItemControlParamDTO dto, int id)
        {
            try
            {
                dto.CertificadoItemControl = null;
                dto.Unidad = null;
                CertificadoItemControlParam entidad = mapper.Map<CertificadoItemControlParam>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<CertificadoItemControlParam>(entidad);
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
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente")]
        public async Task<ActionResult> Baja(int id)
        {
            try
            {
                if (await repositorio.Baja(id, ObtenerUserId()))
                {
                    return Ok($"El CertificadoItemControlParam ha sido dado de baja del sistema");
                }
                return NotFound("El CertificadoItemControlParam a dar de baja no a sido encontrado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpDelete("{id:int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        //Roles = "Admin, BaseDatos, Zona1, Zona2, Frente")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        if (await repositorio.Delete(id))
        //        {
        //            return Ok($"El CertificadoItemControlParam ha sido borrado");
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
