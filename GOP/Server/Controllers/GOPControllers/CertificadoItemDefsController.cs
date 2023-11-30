using AutoMapper;
using GOP.BD.Data.Entity;
using GOP.Repositorio.Repos;
using GOP.Shared.DTOs.Entity;
using GOP.Shared.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;

namespace GOP.Server.Controllers.GOPControllers
{
    [ApiController]
    [Route("GOP/api/CertificadoItemDef")]
    public class CertificadoItemDefsController : GOPBaseController
    {
        private readonly ICertificadoItemDefRepositorio repositorio;
        private readonly ICertificadoItemsRepositorio ciRepositorio;
        private readonly ICertificadoRepositorio certificadoRepositorio;
        private readonly IUnidadesRepositorio unidadesRepositorio;
        private readonly IMapper mapper;

        public CertificadoItemDefsController(ICertificadoItemDefRepositorio repositorio, 
                                             IMapper mapper, 
                                             ICertificadoItemsRepositorio ciRepositorio,
                                             ICertificadoRepositorio certificadoRepositorio,
                                             IUnidadesRepositorio unidadesRepositorio)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.ciRepositorio = ciRepositorio;
            this.certificadoRepositorio = certificadoRepositorio;
            this.unidadesRepositorio = unidadesRepositorio;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoItemDefDTO>>> GetFull()
        {
            try
            {
                List<CertificadoItemDef> lista = await repositorio.GetActivos();

                List<CertificadoItemDefDTO> result = mapper.Map<List<CertificadoItemDefDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{idCertificado:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoItemDefDTO>>> Get(int idCertificado) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetCertificadosDefByIdCertificado(idCertificado);
                if (res == null) return NotFound();

                List<CertificadoItemDefDTO> result = mapper.Map<List<CertificadoItemDefDTO>>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("Reportes/Excel/{certificadoId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<IActionResult> GetReporteCertificadosExcel(int certificadoId) 
        {
            try
            {
                var cert = await certificadoRepositorio.GetCertificadoParaReporteById(certificadoId);

                List<CertificadoItemDef> certDefs = await repositorio.GetCertificadosDefByIdCertificado(certificadoId);
                if (certDefs.Count == 0) return NotFound("El Certificado no posee definitivo.");

                List<ReporteCertificadoItemDefDTO> listaReporte = new List<ReporteCertificadoItemDefDTO>();

                var caratula = string.Empty;
                var periodo = string.Empty;

                foreach (CertificadoItemDef cd in certDefs.OrderBy(e => e.CodItem)) 
                {
                    ReporteCertificadoItemDefDTO reporte = new ReporteCertificadoItemDefDTO();
                    reporte.Expediente = cert.Contrato.NumExpediente;
                    reporte.Zona = cert.Contrato.Caratula.Substring(0, 2);
                    caratula = cert.Contrato.Caratula.Substring(2);
                    reporte.Periodo = DllStatic.PeriodoSeparado(cert.Periodo);
                    reporte.Estado = cert.Estado.ToString();

                    reporte.Fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    reporte.Codigo = cd.CodItem;   
                    reporte.Descripcion = cd.DescItem;
                    Unidad unidad = unidadesRepositorio.GetById(cd.UnidadId).Result;
                    reporte.Un = unidad.CodUnidad;
                    reporte.Coeficiente = cd.Coeficiente;
                    reporte.Total = cd.CantidadTotal;
                    reporte.Informado = cd.CantidadInformada;
                    reporte.Total_Informado = cd.CantidadTotalInformada;
                    reporte.Ejecutado = cd.CantidadAcumulada;
                    reporte.Total_Ejecutado = cd.CantidadTotalEjecutado;
                    reporte.Observacion = cd.Obs;

                    listaReporte.Add(reporte);
                    reporte = null;
                }

                var filename = "Certificado Definitivo -"
                               + $"{caratula}-"
                               + DateTime.Now.ToString("yyyyMMdd HHmm") + ".xlsx"; 

                var exportbytes = ExportToExcel<ReporteCertificadoItemDefDTO>(listaReporte, filename);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                    return Ok($"El CertificadoItemDef ha sido dado de baja del sistema");
                }
                return NotFound("El CertificadoItemDef a dar de baja no a sido encontrado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{idCertificado:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos")]
        public async Task<ActionResult> Delete(int idCertificado)
        {
            try
            {
                Certificado cert = await certificadoRepositorio.GetCertificadoByIdNoFull(idCertificado);
                if (cert == null) return BadRequest("No Existe el certificado solicitado.");

                var itemdefs = repositorio.GetCertificadosDefByIdCertificado(idCertificado);
                if (itemdefs.Result.Count == 0) return NotFound("No existen Certificados Definitivos para el Certificado indicado.");

                string periodoPosterior = DllStatic.CertificadoPosterior(cert.Periodo);
                Certificado cp = await certificadoRepositorio.GetCertificadoPorPeriodo(cert.ContratoId, periodoPosterior);
                if (cp != null)
                {
                    List<CertificadoItemDef> CertDefPosterior = await repositorio.GetCertificadosDefByIdCertificado(cp.Id);
                    if (CertDefPosterior.Count > 0) return BadRequest("Existe un certificado definitivo posterior, se debe borrar y recalcular de nuevo para mantener la consistencia de datos.");
                }
                var citems = ciRepositorio.GetCertificadoItemsSingle(idCertificado);
                if (citems == null) return NotFound("El Certificado indicado no existe.");

                foreach (var ci in citems.Result)
                {
                    if (ci.CertificadoItemDefId != null)
                    {
                        ci.CertificadoItemDefId = null;
                        ActualizaEntidadBase<CertificadoItem>(ci);
                    }
                }

                var result = repositorio.DeleteUpdate(itemdefs.Result, citems.Result);
                if (!result) return BadRequest("Ocurrió un error al intentar eliminar.");

                return Ok("El Certificado Definitivo fue eliminado correctamente.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
