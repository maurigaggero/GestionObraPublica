using AutoMapper;
using GOP.BD.Data.Entity;
using GOP.Repositorio.Repos;
using GOP.Shared.DTOs;
using GOP.Shared.DTOs.Entity;
using GOP.Shared.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace GOP.Server.Controllers.GOPControllers
{
    [ApiController]
    [Route("GOP/api/Certificadoitem")]
    public class CertificadoItemController : GOPBaseController
    {
        private readonly ICertificadoItemsRepositorio repositorio;
        private readonly ICertificadoRepositorio certRepositorio;
        private readonly IContratoItemsRepositorio contratoRepositorio;
        private readonly IContratoEstructuraRepositorio estructuraRepositorio;
        private readonly IFrenteObrasRepositorio frenteRepositorio;
        private readonly IMapper mapper;

        public CertificadoItemController(ICertificadoItemsRepositorio repositorio, IMapper mapper,
                                            ICertificadoRepositorio certRepositorio,
                                            IContratoItemsRepositorio contratoRepositorio,
                                            IContratoEstructuraRepositorio estructuraRepositorio,
                                            IFrenteObrasRepositorio frenteRepositorio)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.certRepositorio = certRepositorio;
            this.contratoRepositorio = contratoRepositorio;
            this.estructuraRepositorio = estructuraRepositorio;
            this.frenteRepositorio = frenteRepositorio;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoItemDTO>>> GetFull()
        {
            try
            {
                List<CertificadoItem> lista = await repositorio.GetActivos();

                List<CertificadoItemDTO> result = mapper.Map<List<CertificadoItemDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getCertificadoItem/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CertificadoItemDTO>>> GetCertificadoItem(int id) //CON INCLUDE
        {
            try
            {
                List<CertificadoItem> lista = await repositorio.GetCertificadoItems(id);

                List<CertificadoItemDTO> result = mapper.Map<List<CertificadoItemDTO>>(lista);
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
        public async Task<ActionResult<CertificadoItemDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetCertificadoItemById(id);
                if (res == null) return NotFound();

                CertificadoItemDTO result = mapper.Map<CertificadoItemDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("Reportes/Excel/Estructura/{estructuraId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult> GetReporteItemsMedidoEstructuraExcel(int estructuraId)
        {
            try
            {
                ContratoEstructura estructura = await estructuraRepositorio.GetById(estructuraId);
                if (estructura == null) return NotFound("La estructura indicada no existe.");

                List<ReporteItemMedidoEstructuraDTO> listaReporte = new List<ReporteItemMedidoEstructuraDTO>();
                List<CertificadoItem> certificadoItems = await repositorio.GetCertificadoItemsByIdEstructura(estructuraId);
                if (certificadoItems == null) return NotFound("No existen Certificados para el Contrato indicado.");
                List<Certificado> certificados = await certRepositorio.GetReporteCertificados();
                int flag = 1;
                ReporteItemMedidoEstructuraDTO repTituloPeriodo = new ReporteItemMedidoEstructuraDTO();
                repTituloPeriodo.Fecha = null;
                foreach (Certificado c in certificados)            //CARGO PERIODOS CERTIFICADOS
                {
                    #region switch carga periodos......
                    switch (flag)
                    {
                        case 1:
                            repTituloPeriodo.Periodo1 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 2:
                            repTituloPeriodo.Periodo2 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 3:
                            repTituloPeriodo.Periodo3 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 4:
                            repTituloPeriodo.Periodo4 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 5:
                            repTituloPeriodo.Periodo5 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 6:
                            repTituloPeriodo.Periodo6 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 7:
                            repTituloPeriodo.Periodo7 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 8:
                            repTituloPeriodo.Periodo8 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 9:
                            repTituloPeriodo.Periodo9 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 10:
                            repTituloPeriodo.Periodo10 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 11:
                            repTituloPeriodo.Periodo11 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 12:
                            repTituloPeriodo.Periodo12 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 13:
                            repTituloPeriodo.Periodo13 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 14:
                            repTituloPeriodo.Periodo14 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 15:
                            repTituloPeriodo.Periodo15 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 16:
                            repTituloPeriodo.Periodo16 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 17:
                            repTituloPeriodo.Periodo17 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                        case 18:
                            repTituloPeriodo.Periodo18 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : c.Periodo;
                            break;
                    }
                    flag++;
                    #endregion
                }
                listaReporte.Add(repTituloPeriodo);

                foreach (CertificadoItem ci in certificadoItems.OrderBy(x => x.CodItem))  //CARGO ITEMS
                {
                    ReporteItemMedidoEstructuraDTO reporte = new ReporteItemMedidoEstructuraDTO();
                    reporte.Fecha = DateOnly.FromDateTime(DateTime.Now);
                    reporte.Codigo = ci.CodItem;
                    reporte.Item = ci.DescItem;
                    if (ci.Unidad.CodUnidad != null)
                        reporte.Unidad = ci.Unidad.CodUnidad;
                    reporte.Estructura = estructura.CodEstructura + "-" + estructura.DescEstructura;

                    foreach (Certificado c in certificados)
                    {
                        List<ContratoItem> contratoItems = await contratoRepositorio.GetContratoItemsByContratoId(c.ContratoId);
                        foreach (ContratoItem item in contratoItems.OrderBy(x => x.CodItem)) //CARGO VALORES DE LOS ITEMS
                        {
                            if (item.Id == ci.ItemContratoId)
                            {
                                reporte.Contrato = item.Contrato.NumExpediente + " " + item.Contrato.Caratula;
                                #region IFS carga periodos donde corresponde
                                if (repTituloPeriodo.Periodo1 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo1) + ci.CantidadMedida;
                                    reporte.Periodo1 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo2 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo2) + ci.CantidadMedida;
                                    reporte.Periodo2 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo3 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo3) + ci.CantidadMedida;
                                    reporte.Periodo3 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo4 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo4) + ci.CantidadMedida;
                                    reporte.Periodo4 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo5 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo5) + ci.CantidadMedida;
                                    reporte.Periodo5 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo6 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo6) + ci.CantidadMedida;
                                    reporte.Periodo6 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo7 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo7) + ci.CantidadMedida;
                                    reporte.Periodo7 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo8 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo8) + ci.CantidadMedida;
                                    reporte.Periodo8 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo9 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo9) + ci.CantidadMedida;
                                    reporte.Periodo9 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo10 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo10) + ci.CantidadMedida;
                                    reporte.Periodo10 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo11 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo11) + ci.CantidadMedida;
                                    reporte.Periodo11 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo12 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo12) + ci.CantidadMedida;
                                    reporte.Periodo12 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo13 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo13) + ci.CantidadMedida;
                                    reporte.Periodo13 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo14 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo14) + ci.CantidadMedida;
                                    reporte.Periodo14 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo15 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo15) + ci.CantidadMedida;
                                    reporte.Periodo15 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo16 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo16) + ci.CantidadMedida;
                                    reporte.Periodo16 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo17 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo17) + ci.CantidadMedida;
                                    reporte.Periodo17 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo18 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo18) + ci.CantidadMedida;
                                    reporte.Periodo18 = res.ToString("0.00");
                                }
                                #endregion
                            }
                        }
                    }
                    listaReporte.Add(reporte);
                }

                var filename = "Items Medidos Estructura_" + estructura.CodEstructura + "_"
                               + DateTime.Now.ToString("yyyyMMdd HHmm") + ".xlsx";

                var exportbytes = ExportToExcel<ReporteItemMedidoEstructuraDTO>(listaReporte, filename);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Reportes/Excel/Frente/{frenteId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult> GetReporteItemsMedidoFrenteExcel(int frenteId)
        {
            try
            {
                List<ReporteItemMedidoFrenteDTO> listaReporte = new List<ReporteItemMedidoFrenteDTO>();

                FrenteObra frente = await frenteRepositorio.GetById(frenteId);
                if (frente == null) return NotFound("El frente de obra indicado no existe.");
                //List<CertificadoItem> certificadoItems = await repositorio.GetCertificadoItemsByIdEstructura(frenteId); //MAL SERGIO
                List<CertificadoItem> certificadoItems = await repositorio.GetCertificadoItemsByIdFrente(frenteId);
                //if (certificadoItems == null) return NotFound("No existen Certificados para el Contrato indicado."); //MAL SERGIO
                if (certificadoItems == null && certificadoItems.Count == 0) return NotFound($"No existen Items Certificados para el el frnete {frente.NombreFrenteObra} indicado.");
                List<Certificado> certificados = await certRepositorio.GetReporteCertificados();

                ReporteItemMedidoFrenteDTO repTituloPeriodo = new ReporteItemMedidoFrenteDTO();
                ReporteItemMedidoFrenteDTO reporte = new ReporteItemMedidoFrenteDTO();

                repTituloPeriodo.Fecha = null;
                #region Borrar
                //    flag++;
                //    #endregion
                //}
                //listaReporte.Add(repTituloPeriodo);
                #endregion
                int col = 1;
                string per = "";
                int contratoItemId = 0;
                bool flagInicio = true;
                foreach (CertificadoItem ci in certificadoItems)  //CARGO ITEMS
                {
                    if (flagInicio)
                    {
                        per = DllStatic.PeriodoSeparado(ci.Certificado.Periodo);
                        contratoItemId = ci.ItemContratoId;
                        repTituloPeriodo = LlenaTituloPeriodo(repTituloPeriodo, col, per);
                    }
                    else
                    {
                        repTituloPeriodo = LlenaTituloPeriodo(repTituloPeriodo, col, per);
                    }
                    //HASTA ACA LLEGUÉ
                    reporte.Fecha = DateOnly.FromDateTime(DateTime.Now);
                    reporte.Codigo = ci.CodItem;
                    reporte.Item = ci.DescItem;
                    if (ci.Unidad.CodUnidad != null)
                        reporte.Unidad = ci.Unidad.CodUnidad;
                    reporte.Frente = frente.CodFrenteObra + "-" + frente.NombreFrenteObra;

                    foreach (Certificado c in certificados)
                    {
                        List<ContratoItem> contratoItems = await contratoRepositorio.GetContratoItemsByContratoId(c.ContratoId);
                        foreach (ContratoItem item in contratoItems.OrderBy(x => x.CodItem)) //CARGO VALORES DE LOS ITEMS
                        {
                            if (item.Id == ci.ItemContratoId)
                            {
                                reporte.Contrato = item.Contrato.NumExpediente + " " + item.Contrato.Caratula;

                                #region IFS carga periodos donde corresponde
                                if (repTituloPeriodo.Periodo1 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo1) + ci.CantidadMedida;
                                    reporte.Periodo1 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo2 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo2) + ci.CantidadMedida;
                                    reporte.Periodo2 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo3 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo3) + ci.CantidadMedida;
                                    reporte.Periodo3 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo4 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo4) + ci.CantidadMedida;
                                    reporte.Periodo4 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo5 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo5) + ci.CantidadMedida;
                                    reporte.Periodo5 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo6 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo6) + ci.CantidadMedida;
                                    reporte.Periodo6 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo7 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo7) + ci.CantidadMedida;
                                    reporte.Periodo7 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo8 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo8) + ci.CantidadMedida;
                                    reporte.Periodo8 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo9 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo9) + ci.CantidadMedida;
                                    reporte.Periodo9 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo10 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo10) + ci.CantidadMedida;
                                    reporte.Periodo10 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo11 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo11) + ci.CantidadMedida;
                                    reporte.Periodo11 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo12 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo12) + ci.CantidadMedida;
                                    reporte.Periodo12 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo13 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo13) + ci.CantidadMedida;
                                    reporte.Periodo13 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo14 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo14) + ci.CantidadMedida;
                                    reporte.Periodo14 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo15 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo15) + ci.CantidadMedida;
                                    reporte.Periodo15 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo16 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo16) + ci.CantidadMedida;
                                    reporte.Periodo16 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo17 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo17) + ci.CantidadMedida;
                                    reporte.Periodo17 = res.ToString("0.00");
                                }
                                else if (repTituloPeriodo.Periodo18 == c.Periodo)
                                {
                                    decimal res = Convert.ToDecimal(reporte.Periodo18) + ci.CantidadMedida;
                                    reporte.Periodo18 = res.ToString("0.00");
                                }
                                #endregion
                            }
                        }
                    }
                    listaReporte.Add(reporte);
                }

                var filename = "Items Medidos Frente_" + frente.CodFrenteObra + "_"
                               + DateTime.Now.ToString("yyyyMMdd HHmm") + ".xlsx";

                var exportbytes = ExportToExcel<ReporteItemMedidoFrenteDTO>(listaReporte, filename);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private ReporteItemMedidoFrenteDTO  LlenaTituloPeriodo(ReporteItemMedidoFrenteDTO repTituloPeriodo,
                                                               int columna,
                                                               string periodo)
        {
            switch (columna) 
            {
                case 1:
                    repTituloPeriodo.Periodo1 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 2:
                    repTituloPeriodo.Periodo2 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 3:
                    repTituloPeriodo.Periodo3 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 4:
                    repTituloPeriodo.Periodo4 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 5:
                    repTituloPeriodo.Periodo5 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 6:
                    repTituloPeriodo.Periodo6 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 7:
                    repTituloPeriodo.Periodo7 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 8:
                    repTituloPeriodo.Periodo8 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 9:
                    repTituloPeriodo.Periodo9 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 10:
                    repTituloPeriodo.Periodo10 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 11:
                    repTituloPeriodo.Periodo11 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 12:
                    repTituloPeriodo.Periodo12 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 13:
                    repTituloPeriodo.Periodo13 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 14:
                    repTituloPeriodo.Periodo14 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 15:
                    repTituloPeriodo.Periodo15 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 16:
                    repTituloPeriodo.Periodo16 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 17:
                    repTituloPeriodo.Periodo17 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
                case 18:
                    repTituloPeriodo.Periodo18 = String.IsNullOrEmpty(periodo) ? String.Empty : DllStatic.PeriodoSeparado(periodo);
                    break;
            }
            return repTituloPeriodo;
        }

        //[HttpGet("Reportes/Excel/Frente/{frenteId:int}")]                                               //MAL SERGIO REEMPLAZADO
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        //Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        //public async Task<ActionResult> GetReporteItemsMedidoFrenteExcel(int frenteId)
        //{
        //    try
        //    {
        //        List<ReporteItemMedidoFrenteDTO> listaReporte = new List<ReporteItemMedidoFrenteDTO>();

        //        FrenteObra frente = await frenteRepositorio.GetById(frenteId);
        //        if (frente == null) return NotFound("El frente de obra indicado no existe.");
        //        //List<CertificadoItem> certificadoItems = await repositorio.GetCertificadoItemsByIdEstructura(frenteId); //MAL SERGIO
        //        List<CertificadoItem> certificadoItems = await repositorio.GetCertificadoItemsByIdFrente(frenteId);
        //        //if (certificadoItems == null) return NotFound("No existen Certificados para el Contrato indicado."); //MAL SERGIO
        //        if (certificadoItems == null && certificadoItems.Count == 0) return NotFound("No existen Certificados para el Contrato indicado.");
        //        List<Certificado> certificados = await certRepositorio.GetReporteCertificados();

        //        int flag = 1;
        //        ReporteItemMedidoFrenteDTO repTituloPeriodo = new ReporteItemMedidoFrenteDTO();
        //        repTituloPeriodo.Fecha = null;
        //        foreach (Certificado c in certificados)            //CARGO PERIODOS CERTIFICADOS
        //        {
        //            #region switch carga periodos......
        //            switch (flag) //MAL SERGIO
        //            {
        //                case 1:
        //                    repTituloPeriodo.Periodo1 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 2:
        //                    repTituloPeriodo.Periodo2 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 3:
        //                    repTituloPeriodo.Periodo3 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 4:
        //                    repTituloPeriodo.Periodo4 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 5:
        //                    repTituloPeriodo.Periodo5 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 6:
        //                    repTituloPeriodo.Periodo6 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 7:
        //                    repTituloPeriodo.Periodo7 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 8:
        //                    repTituloPeriodo.Periodo8 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 9:
        //                    repTituloPeriodo.Periodo9 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 10:
        //                    repTituloPeriodo.Periodo10 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 11:
        //                    repTituloPeriodo.Periodo11 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 12:
        //                    repTituloPeriodo.Periodo12 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 13:
        //                    repTituloPeriodo.Periodo13 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 14:
        //                    repTituloPeriodo.Periodo14 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 15:
        //                    repTituloPeriodo.Periodo15 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 16:
        //                    repTituloPeriodo.Periodo16 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 17:
        //                    repTituloPeriodo.Periodo17 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //                case 18:
        //                    repTituloPeriodo.Periodo18 = String.IsNullOrEmpty(c.Periodo) ? String.Empty : DllStatic.PeriodoSeparado(c.Periodo);
        //                    break;
        //            }
        //            flag++;
        //            #endregion
        //        }
        //        listaReporte.Add(repTituloPeriodo);

        //        foreach (CertificadoItem ci in certificadoItems.OrderBy(x => x.CodItem))  //CARGO ITEMS
        //        {
        //            ReporteItemMedidoFrenteDTO reporte = new ReporteItemMedidoFrenteDTO();
        //            reporte.Fecha = DateOnly.FromDateTime(DateTime.Now);
        //            reporte.Codigo = ci.CodItem;
        //            reporte.Item = ci.DescItem;
        //            if (ci.Unidad.CodUnidad != null)
        //                reporte.Unidad = ci.Unidad.CodUnidad;
        //            reporte.Frente = frente.CodFrenteObra + "-" + frente.NombreFrenteObra;

        //            foreach (Certificado c in certificados)
        //            {
        //                List<ContratoItem> contratoItems = await contratoRepositorio.GetContratoItemsByContratoId(c.ContratoId);
        //                foreach (ContratoItem item in contratoItems.OrderBy(x => x.CodItem)) //CARGO VALORES DE LOS ITEMS
        //                {
        //                    if (item.Id == ci.ItemContratoId)
        //                    {
        //                        reporte.Contrato = item.Contrato.NumExpediente + " " + item.Contrato.Caratula;

        //                        #region IFS carga periodos donde corresponde
        //                        if (repTituloPeriodo.Periodo1 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo1) + ci.CantidadMedida;
        //                            reporte.Periodo1 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo2 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo2) + ci.CantidadMedida;
        //                            reporte.Periodo2 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo3 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo3) + ci.CantidadMedida;
        //                            reporte.Periodo3 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo4 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo4) + ci.CantidadMedida;
        //                            reporte.Periodo4 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo5 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo5) + ci.CantidadMedida;
        //                            reporte.Periodo5 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo6 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo6) + ci.CantidadMedida;
        //                            reporte.Periodo6 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo7 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo7) + ci.CantidadMedida;
        //                            reporte.Periodo7 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo8 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo8) + ci.CantidadMedida;
        //                            reporte.Periodo8 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo9 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo9) + ci.CantidadMedida;
        //                            reporte.Periodo9 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo10 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo10) + ci.CantidadMedida;
        //                            reporte.Periodo10 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo11 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo11) + ci.CantidadMedida;
        //                            reporte.Periodo11 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo12 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo12) + ci.CantidadMedida;
        //                            reporte.Periodo12 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo13 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo13) + ci.CantidadMedida;
        //                            reporte.Periodo13 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo14 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo14) + ci.CantidadMedida;
        //                            reporte.Periodo14 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo15 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo15) + ci.CantidadMedida;
        //                            reporte.Periodo15 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo16 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo16) + ci.CantidadMedida;
        //                            reporte.Periodo16 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo17 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo17) + ci.CantidadMedida;
        //                            reporte.Periodo17 = res.ToString("0.00");
        //                        }
        //                        else if (repTituloPeriodo.Periodo18 == c.Periodo)
        //                        {
        //                            decimal res = Convert.ToDecimal(reporte.Periodo18) + ci.CantidadMedida;
        //                            reporte.Periodo18 = res.ToString("0.00");
        //                        }
        //                        #endregion
        //                    }
        //                }
        //            }
        //            listaReporte.Add(reporte);
        //        }

        //        var filename = "Items Medidos Frente_" + frente.CodFrenteObra + "_"
        //                       + DateTime.Now.ToString("yyyyMMdd HHmm") + ".xlsx";

        //        var exportbytes = ExportToExcel<ReporteItemMedidoFrenteDTO>(listaReporte, filename);
        //        return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente")]
        public async Task<ActionResult<List<CertificadoItemDTO>>> Post(CertificadoItemDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                CertificadoItem ci = new CertificadoItem();
                ci.CertificadoId = dto.CertificadoId;
                ci.Estado = dto.Estado;
                ci.CantidadTotal = dto.CantidadTotal;
                ci.CantidadMedida = dto.CantidadMedida;
                ci.CantidadInformada = dto.CantidadInformada;
                ci.Coeficiente = dto.Coeficiente;
                ci.FechaMedicion = dto.FechaMedicion;
                ci.Obs = dto.Obs;
                //ITEM DEL CONTRATO
                ContratoItem item = await repositorio.GetItemDelCertificado(dto.ItemContratoId);
                ci.ItemContratoId = item.Id;
                ci.DescItem = item.DescItem;
                ci.CodItem = item.CodItem;

                List<CertificadoItemControl> listCertificadoitemcontrol = new();
                foreach (ContratoItemControl itemcontrol in item.ContratoItemControls)
                {
                    //ITEM CONTROL
                    CertificadoItemControl Certificadoitemcontrol = new();
                    Certificadoitemcontrol.ContratoItemControlId = itemcontrol.Id;
                    Certificadoitemcontrol.DescControl = itemcontrol.DescControl;

                    List<CertificadoItemControlParam> Certificadoparametros = new();
                    foreach (ContratoItemControlParam itemparam in itemcontrol.Parametros)
                    {
                        //PARAMETROS
                        CertificadoItemControlParam Certificadoparam = new();
                        Certificadoparam.ContratoItemControlParamId = itemparam.Id;
                        Certificadoparam.Parametro = itemparam.Parametro;
                        Certificadoparam.Descripción = itemparam.Descripción;
                        Certificadoparam.UnidadId = itemparam.UnidadId;

                        #region Actualiza idcrear entidad Certificadoparam
                        ActualizaEntidadBase<CertificadoItemControlParam>(Certificadoparam);
                        #endregion

                        Certificadoparametros.Add(Certificadoparam);
                    }
                    Certificadoitemcontrol.Parametros = Certificadoparametros;

                    #region Actualiza idcrear entidad Certificadoitemcontrol
                    ActualizaEntidadBase<CertificadoItemControl>(Certificadoitemcontrol);
                    #endregion

                    listCertificadoitemcontrol.Add(Certificadoitemcontrol);
                }
                ci.CertificadoItemControls = listCertificadoitemcontrol;
                ci.FrenteObraId = dto.FrenteObraId;
                ci.UnidadId = dto.UnidadId;
                if (dto.ContratoEstructuraId != 0)
                    ci.ContratoEstructuraId = dto.ContratoEstructuraId;

                #region Actualiza idcrear entidad ci
                ActualizaEntidadBase<CertificadoItem>(ci);
                #endregion

                int result = await repositorio.Insert(ci);
                if (result > 0)
                {
                    CertificadoItem objResult = await repositorio.GetById(result);

                    CertificadoItemDTO dtoResult = mapper.Map<CertificadoItemDTO>(objResult);
                    return Ok(dto);
                }
                else
                    return BadRequest("No se pudo agregar el tipo de estado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente")]
        public async Task<ActionResult<bool>> Put(CertificadoItemDTO dto, int id)
        {
            try
            {
                dto.ItemContrato = null;
                dto.Certificado = null;
                dto.CertificadoItemControls = null;
                dto.ContratoEstructura = null;
                dto.FrenteObra = null;
                dto.Unidad = null;
                CertificadoItem CertificadoItem = mapper.Map<CertificadoItem>(dto);

                #region Actualiza idcrear entidad CertificadoItem
                ActualizaEntidadBase<CertificadoItem>(CertificadoItem);
                #endregion

                var resp = await repositorio.Update(CertificadoItem, id);
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
                    return Ok($"El CertificadoItem ha sido dado de baja del sistema");
                }
                return NotFound("El CertificadoItem a dar de baja no a sido encontrado");
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
        //            return Ok($"El CertificadoItem ha sido borrado");
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
