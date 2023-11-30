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
    [Route("GOP/api/CertificadoItemControlDocs")]
    public class CertificadoItemControlDocsController : GOPBaseController
    {
        private readonly ICertificadoItemControlDocsRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;

        public CertificadoItemControlDocsController(ICertificadoItemControlDocsRepositorio repositorio, 
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
        public async Task<ActionResult<List<CertificadoItemControlDocDTO>>> GetFull()
        {
            try
            {
                List<CertificadoItemControlDoc> lista = await repositorio.GetFull();

                List<CertificadoItemControlDocDTO> result = mapper.Map<List<CertificadoItemControlDocDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getCertificadoItemControlDoc/{itemControlId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoItemControlDocDTO>>> GetCertificadoItemControlDoc(int itemControlId) //CON INCLUDE
        {
            try
            {
                List<CertificadoItemControlDoc> lista = await repositorio.GetCertificadoItemControlDocs(itemControlId);

                List<CertificadoItemControlDocDTO> result = mapper.Map<List<CertificadoItemControlDocDTO>>(lista);
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
        public async Task<ActionResult<CertificadoItemControlDocDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetCertificadoItemControlDocById(id);
                if (res == null) return NotFound();

                CertificadoItemControlDocDTO result = mapper.Map<CertificadoItemControlDocDTO>(res);
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
        public async Task<ActionResult<int>> Post(CertificadoItemControlDocDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                CertificadoItemControlDoc entidad = mapper.Map<CertificadoItemControlDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.GuardarArchivo(archivo, dto.Extension, "ItemControles");
                }

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<CertificadoItemControlDoc>(entidad);
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
        public async Task<ActionResult<bool>> Put(CertificadoItemControlDocDTO dto, int id)
        {
            try
            {
                dto.CertificadoItemControl = null;
                CertificadoItemControlDoc entidad = mapper.Map<CertificadoItemControlDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.EditarArchivo(archivo, dto.Extension, "ItemControles", entidad.PathDoc);
                }

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<CertificadoItemControlDoc>(entidad);
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
                //CertificadoItemControlDocDTO item = Get(id).Result.Value;
                //await almacenadorArchivos.EliminarArchivo(item.PathDoc);
                if (await repositorio.Baja(id, ObtenerUserId()))
                {
                    return Ok($"El CertificadoItemControlDoc ha sido dado de baja del sistema");
                }
                return NotFound("El CertificadoItemControlDoc a dar de baja no a sido encontrado");
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
        //        CertificadoItemControlDocDTO item = Get(id).Result.Value;
        //        await almacenadorArchivos.EliminarArchivo(item.PathDoc);
        //        if (await repositorio.Delete(id))
        //        {
        //            return Ok($"El CertificadoItemControlDoc ha sido borrado");
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
