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
    [Route("GOP/api/Certificadoitemcontrol")]
    public class CertificadoItemControlsController : GOPBaseController
    {
        private readonly ICertificadoItemControlsRepositorio repositorio;
        private readonly IMapper mapper;

        public CertificadoItemControlsController(ICertificadoItemControlsRepositorio repositorio, 
                                                 IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoItemControlDTO>>> GetFull()
        {
            try
            {
                List<CertificadoItemControl> lista = await repositorio.GetActivos();

                List<CertificadoItemControlDTO> result = mapper.Map<List<CertificadoItemControlDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getCertificadoItemControl")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoItemControlDTO>>> GetCertificadoItemControl() //CON INCLUDE
        {
            try
            {
                List<CertificadoItemControl> lista = await repositorio.GetCertificadoItemControls();

                List<CertificadoItemControlDTO> result = mapper.Map<List<CertificadoItemControlDTO>>(lista);
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
        public async Task<ActionResult<CertificadoItemControlDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetCertificadoItemControlById(id);
                if (res == null) return NotFound();

                CertificadoItemControlDTO result = mapper.Map<CertificadoItemControlDTO>(res);
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente")]
        public async Task<ActionResult<int>> Post(CertificadoItemControlDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                CertificadoItemControl entidad = mapper.Map<CertificadoItemControl>(dto);

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<CertificadoItemControl>(entidad);
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
        public async Task<ActionResult<bool>> Put(CertificadoItemControlDTO dto, int id)
        {
            try
            {
                dto.Documentos = null;
                dto.Parametros = null;
                dto.CertificadoItem = null;
                dto.ContratoItemControl = null;
                CertificadoItemControl entidad = mapper.Map<CertificadoItemControl>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<CertificadoItemControl>(entidad);
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
                    return Ok($"El CertificadoItemControl ha sido dado de baja del sistema");
                }
                return NotFound("El CertificadoItemControl a dar de baja no a sido encontrado");
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
        //            return Ok($"El CertificadoItemControl ha sido borrado");
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
