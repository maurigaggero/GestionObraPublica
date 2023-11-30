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
    [Route("GOP/api/CertificadoDoc")]
    public class CertificadoDocController : GOPBaseController
    {
        private readonly ICertificadoDocRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;

        public CertificadoDocController(ICertificadoDocRepositorio repositorio, 
                                        IMapper mapper, 
                                        IAlmacenadorArchivos almacenadorArchivos)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet("getfull/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoDocDTO>>> GetFull(int id)
        {
            try
            {
                List<CertificadoDoc> lista = await repositorio.GetCertificadosDocById(id);

                List<CertificadoDocDTO> result = mapper.Map<List<CertificadoDocDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getCertificadoDoc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoDocDTO>>> GetCertificadoDoc() //CON INCLUDE
        {
            try
            {
                List<CertificadoDoc> lista = await repositorio.GetCertificadoDocs();

                List<CertificadoDocDTO> result = mapper.Map<List<CertificadoDocDTO>>(lista);
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
        public async Task<ActionResult<CertificadoDocDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetCertificadoDocById(id);
                if (res == null) return NotFound();

                CertificadoDocDTO result = mapper.Map<CertificadoDocDTO>(res);
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
        public async Task<ActionResult<int>> Post(CertificadoDocDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                CertificadoDoc entidad = mapper.Map<CertificadoDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.GuardarArchivo(archivo, dto.Extension, "Certificados");
                }

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<CertificadoDoc>(entidad);
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
        public async Task<ActionResult<bool>> Put(CertificadoDocDTO dto, int id)
        {
            try
            {
                dto.Certificado = null;
                CertificadoDoc entidad = mapper.Map<CertificadoDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.EditarArchivo(archivo, dto.Extension, "Certificados", entidad.PathDoc);
                }

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<CertificadoDoc>(entidad);
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
                //CertificadoDocDTO doc = Get(id).Result.Value;
                //await almacenadorArchivos.EliminarArchivo(doc.PathDoc);
                if (await repositorio.Baja(id, ObtenerUserId()))
                {
                    return Ok($"El CertificadoDoc ha sido dado de baja del sistema");
                }
                return NotFound("El CertificadoDoc a dar de baja no a sido encontrado");
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
        //        CertificadoDocDTO doc = Get(id).Result.Value;
        //        await almacenadorArchivos.EliminarArchivo(doc.PathDoc);
        //        if (await repositorio.Delete(id))
        //        {
        //            return Ok($"El CertificadoDoc ha sido borrado");
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
