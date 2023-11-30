using AutoMapper;
using GOP.BD.Data;
using GOP.BD.Data.Entity;
using GOP.Repositorio.Repos;
using GOP.Shared.DTOs.Entity;
using GOP.Shared.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Index.HPRtree;
using System.Drawing;

namespace GOP.Server.Controllers.GOPControllers
{
    [ApiController]
    [Route("GOP/api/Certificado")]
    public class CertificadoController : GOPBaseController
    {
        private readonly ICertificadoRepositorio repositorio;
        private readonly ICertificadoItemDefRepositorio itemDefRepositorio;
        private readonly IContratoItemsRepositorio contratoItemsRepositorio;
        private readonly ICertificadoItemsRepositorio certificadoItemsRepositorio;
        private readonly IMapper mapper;

        public CertificadoController(ICertificadoRepositorio repositorio,
                                     IMapper mapper,
                                     ICertificadoItemDefRepositorio itemDefRepositorio,
                                     IContratoItemsRepositorio contratoItemsRepositorio,
                                     ICertificadoItemsRepositorio certificadoItemsRepositorio)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.itemDefRepositorio = itemDefRepositorio;
            this.contratoItemsRepositorio = contratoItemsRepositorio;
            this.certificadoItemsRepositorio = certificadoItemsRepositorio;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoDTO>>> GetFull()
        {
            try
            {
                List<Certificado> lista = await repositorio.GetActivos();

                List<CertificadoDTO> result = mapper.Map<List<CertificadoDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getCertificado")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoDTO>>> GetCertificado() //CON INCLUDE
        {
            try
            {
                List<Certificado> lista = await repositorio.GetCertificados();

                List<CertificadoDTO> result = mapper.Map<List<CertificadoDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("Reportes")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoDTO>>> GetReporteCertificados()
        {
            try
            {
                List<Certificado> lista = await repositorio.GetReporteCertificados();

                List<CertificadoDTO> result = mapper.Map<List<CertificadoDTO>>(lista);
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
        public async Task<IActionResult> GetReporteCertificadosExcel(int certificadoId)  //Sergio
        {
            try
            {
                List<ReporteCertificadoDTO> listaReporte = new List<ReporteCertificadoDTO>();

                Certificado cert = await repositorio.GetCertificadoParaReporteById(certificadoId);
                if (cert == null) return NotFound("El Certificado indicado no existe.");

                foreach (CertificadoItem ci in cert.CertificadoItems.OrderBy(x => x.CodItem).ThenBy(x => x.FechaMedicion)) //Sergio
                {
                    ReporteCertificadoDTO reporte = new ReporteCertificadoDTO();//Sergio
                    reporte.Expediente = cert.Contrato.NumExpediente;
                    reporte.Zona = cert.Contrato.Caratula.Substring(0, 2);
                    reporte.Periodo = DllStatic.PeriodoSeparado(cert.Periodo);
                    reporte.Fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    reporte.Estado_Certificado = cert.Estado.ToString();
                    reporte.Codigo = ci.CodItem;
                    reporte.Descripcion = ci.DescItem;
                    if (ci.Unidad.CodUnidad != null)
                    {
                        reporte.Un = ci.Unidad.CodUnidad;
                    }
                    reporte.Medicion = ci.FechaMedicion.ToString("dd/MM/yyyy");
                    reporte.Medido = ci.CantidadMedida;
                    reporte.Informado = ci.CantidadInformada;
                    reporte.Total = ci.CantidadTotal;
                    string codFrente = "";
                    if (ci.FrenteObra != null)
                    {
                        codFrente = ci.FrenteObra.CodFrenteObra;
                    }
                    reporte.Frente = codFrente;
                    string codEstructura = "";
                    if(ci.ContratoEstructura!=null)
                    {
                        codEstructura= ci.ContratoEstructura.CodEstructura;
                    }
                    reporte.Estructura = codEstructura;
                    reporte.Estado_Medicion = ci.Estado.ToString();
                    reporte.Observacion = ci.Obs;
                    listaReporte.Add(reporte);
                    reporte = null;
                }

                var filename = "Certificado Medido Provisorio -"
                               + $"{cert.Contrato.Caratula.Substring(0, 2)}-("
                               + $"{DllStatic.PeriodoSeparado(cert.Periodo, "-")})-"
                               + DateTime.Now.ToString("yyyyMMdd HHmm") + ".xlsx"; //Sergio

                var exportbytes = ExportToExcel<ReporteCertificadoDTO>(listaReporte, filename);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Reportes/Control")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoDTO>>> GetReporteControlCertificados()
        {
            try
            {
                List<Certificado> lista = await repositorio.GetReporteControlCertificados();

                List<CertificadoDTO> result = mapper.Map<List<CertificadoDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<CertificadoDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetCertificadoById(id);
                if (res == null) return NotFound();

                CertificadoDTO result = mapper.Map<CertificadoDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetByIdNoFul/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<CertificadoDTO>> GetByIdNoFull(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetCertificadoByIdNoFull(id);
                if (res == null) return NotFound();

                CertificadoDTO result = mapper.Map<CertificadoDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getCertificadoPorContrato/{idContrato:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoDTO>>> GetCertificadoPorContrato(int idContrato) //CON INCLUDE
        {
            try
            {
                List<Certificado> lista = await repositorio.GetCertificadosPorContrato(idContrato);

                List<CertificadoDTO> result = mapper.Map<List<CertificadoDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2")]
        public async Task<ActionResult<int>> Post(CertificadoDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                Certificado entidad = mapper.Map<Certificado>(dto);

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<Certificado>(entidad);
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

        [HttpPost("def/{certificadoId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                Roles = "Admin, BaseDatos, Zona1, Zona2")]
        public async Task<ActionResult<int>> Post(int certificadoId)
        {
            try
            {
                //controla si existe certificado
                if (certificadoId == 0) return BadRequest("Datos incorrectos. No se seleccionó un certificado.");
                Certificado cert = await repositorio.GetCertificadoById(certificadoId);
                if (cert == null) return NotFound("El Certificado indicado no existe.");

                //controla si hay items en el contrato
                List<ContratoItem> items = await contratoItemsRepositorio.GetContratoItemsByContratoId(cert.ContratoId);
                if (items == null) return NotFound("No existen Items para Certificar.");

                //controla si ya existe el definitivo
                CertificadoItemDef entidad = new CertificadoItemDef();
                List<CertificadoItemDef> CertDef = await itemDefRepositorio.GetCertificadosDefByIdCertificado(certificadoId);
                if (CertDef.Count > 0) return BadRequest("El certificado seleccionado ya posee definitivo! Debe eliminarlo para volverlo a generar.");

                //controla si hay definitivo posterior
                string periodoPosterior = DllStatic.CertificadoPosterior(cert.Periodo);
                Certificado cp = await repositorio.GetCertificadoPorPeriodo(cert.ContratoId, periodoPosterior);
                if (cp != null)
                {
                    List<CertificadoItemDef> CertDefPosterior = await itemDefRepositorio
                                                     .GetCertificadosDefByIdCertificado(cp.Id);
                    if (CertDefPosterior.Count > 0) return BadRequest("Existe un certificado definitivo posterior, se debe borrar y recalcular de nuevo para mantener la consistencia de datos.");
                }

                //controla si hay definitivo anterior sin cerrar
                string periodoAnterior = DllStatic.CertificadoAnterior(cert.Periodo);
                Certificado cAnterior = await repositorio.GetCertificadoPorPeriodo(cert.ContratoId, periodoAnterior);
                List<CertificadoItemDef> CertDefAnterior = new();
                if (cAnterior != null)
                {
                    CertDefAnterior = await itemDefRepositorio
                                                     .GetCertificadosDefByIdCertificado(cAnterior.Id);
                    if (CertDefAnterior.Count == 0) return BadRequest("Existe un certificado anterior sin definitivo, se debe generar anteriormente para conservar la consistencia de datos.");
                }

                int estadoInsert = 0;
                bool rtaUpdateCID = false;
                bool rtaUpdateCI = false;

                foreach (ContratoItem i in items)
                {
                    entidad.CertificadoId = certificadoId;
                    entidad.ItemContratoId = i.Id;
                    entidad.CantidadAcumulada = 0;
                    entidad.CantidadInformada = 0;
                    entidad.CantidadTotal = i.CantidadTotal; //CANTIDAD INFORMADA DE CONTRATO 
                    entidad.CodItem = i.CodItem;
                    entidad.DescItem = i.DescItem;
                    entidad.Coeficiente = i.Coeficiente;
                    entidad.UnidadId = i.Item.UnidadId;
                    entidad.Estado = 0;
                    entidad.Id = 0;

                    #region Actualiza idcrear entidad
                    ActualizaEntidadBase<CertificadoItemDef>(entidad);
                    #endregion
                    estadoInsert = await itemDefRepositorio.Insert(entidad);
                }

                if (estadoInsert > 0)
                {
                    //Lista de Certificados Definitivos correspondientes al Certificado parametrizado (certificados def actuales)
                    List<CertificadoItemDef> CertDefs = await itemDefRepositorio.GetCertificadosDefByIdCertificado(certificadoId);

                    foreach (CertificadoItemDef cidef in CertDefs)
                    {
                        foreach (CertificadoItem ci in cert.CertificadoItems)
                        {
                            if (ci.ItemContratoId == cidef.ItemContratoId)
                            {
                                cidef.CantidadInformada += ci.CantidadInformada;
                                cidef.CantidadTotalInformada += ci.CantidadInformada;

                                cidef.CantidadAcumulada += ci.CantidadMedida;
                                cidef.CantidadTotalEjecutado += ci.CantidadMedida;

                                cidef.CantidadTotal = ci.CantidadTotal;
                                cidef.UnidadId = ci.UnidadId;
                                cidef.Estado = ci.Estado;

                                #region Actualiza idmodif cidef
                                ActualizaEntidadBase<CertificadoItemDef>(cidef);
                                #endregion
                                rtaUpdateCID = await itemDefRepositorio.Update(cidef, cidef.Id);

                                ci.CertificadoItemDefId = cidef.Id;

                                #region Actualiza idmodif ci
                                ActualizaEntidadBase<CertificadoItem>(ci);
                                #endregion
                                ci.Certificado = null;
                                rtaUpdateCI = await certificadoItemsRepositorio.Update(ci, ci.Id);
                            }
                        }
                    }

                    if (CertDefAnterior.Count > 0)
                    {
                        foreach (var ciAnt in CertDefAnterior)
                        {
                            foreach (var ciActual in CertDefs)
                            {
                                if (ciAnt.ItemContratoId == ciActual.ItemContratoId)
                                {
                                    ciActual.CantidadTotalInformada += ciAnt.CantidadTotalInformada;
                                    ciActual.CantidadTotalEjecutado += ciAnt.CantidadTotalEjecutado;

                                    #region Actualiza idmodif cidef Actuales
                                    ActualizaEntidadBase<CertificadoItemDef>(ciActual);
                                    #endregion
                                    rtaUpdateCID = await itemDefRepositorio.Update(ciActual, ciActual.Id);
                                }
                            }
                        }
                    }
                    
                    if (rtaUpdateCID && rtaUpdateCI)
                        return Ok("Se han generado correctamente todos los certificados definitivos de cada item.");
                    else
                        return BadRequest("Ocurrió un error al generar los certificados definitivos");
                }
                else return BadRequest("Ocurrió un error al generar los certificados definitivos");
            }
            catch (Exception e)
            {
                return BadRequest(@$"Error: {e.Message}\r\nINNER: {e.InnerException.Message}");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2")]
        public async Task<ActionResult<bool>> Put(CertificadoDTO dto, int id)
        {
            try
            {
                dto.CertificadoItems = null;
                dto.Eventos = null;
                dto.ZonaProfesional = null;
                dto.Contrato = null;
                Certificado entidad = mapper.Map<Certificado>(dto);

                #region Actualiza idModif entidad
                ActualizaEntidadBase<Certificado>(entidad);
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
        Roles = "Admin, BaseDatos, Zona1, Zona2")]
        public async Task<ActionResult> Baja(int id)
        {
            try
            {
                if (await repositorio.Baja(id, ObtenerUserId()))
                {
                    return Ok($"El Certificado ha sido dado de baja del sistema");
                }
                return NotFound("El Certificado a dar de baja no a sido encontrado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpDelete("{id:int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        //Roles = "Admin, BaseDatos, Zona1, Zona2")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        if (await repositorio.Delete(id))
        //        {
        //            return Ok($"El Certificado ha sido borrado");
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
