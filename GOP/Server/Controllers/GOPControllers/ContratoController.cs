using AutoMapper;
using GOP.BD.Data;
using GOP.BD.Data.Entity;
using GOP.Repositorio.Repos;
using GOP.Shared.DTOs;
using GOP.Shared.DTOs.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Index.HPRtree;

namespace GOP.Server.Controllers.GOPControllers
{
    [ApiController]
    [Route("GOP/api/contrato")]
    public class ContratoController : GOPBaseController
    {
        private readonly IContratoRepositorio repositorio;
        private readonly ICertificadoRepositorio certificadoRepositorio;
        private readonly ICertificadoItemDefRepositorio certDefsRepositorio;
        private readonly IContratoItemsRepositorio contratoItemsRepositorio;
        private readonly IMapper mapper;

        public ContratoController(IContratoRepositorio repositorio,
                                  ICertificadoRepositorio certificadoRepositorio,
                                  IContratoItemsRepositorio contratoItemsRepositorio,
                                  ICertificadoItemDefRepositorio certDefsRepositorio,
                                  IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.certificadoRepositorio = certificadoRepositorio;
            this.contratoItemsRepositorio = contratoItemsRepositorio;
            this.certDefsRepositorio = certDefsRepositorio;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoDTO>>> GetFull()
        {
            try
            {
                List<Contrato> lista = await repositorio.GetActivos();

                List<ContratoDTO> result = mapper.Map<List<ContratoDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getContrato")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoDTO>>> GetContrato()
        {
            try
            {
                List<Contrato> lista = await repositorio.GetContratos();

                List<ContratoDTO> result = mapper.Map<List<ContratoDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getNoFull/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<ContratoDTO>> getNoFull(int id) //SIN INCLUDE sergio
        {
            try
            {
                var res = await repositorio.GetContratoByIdNoFull(id);
                if (res == null) return NotFound();

                ContratoDTO result = mapper.Map<ContratoDTO>(res);
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
        public async Task<ActionResult<ContratoDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetContratoById(id);
                if (res == null) return NotFound();

                ContratoDTO result = mapper.Map<ContratoDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetCaratula/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<string>> GetCaratula(int id)  //sergio
        {
            try
            {
                string res = "";
                res = await repositorio.GetCaratula(id);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("Reportes/Excel/ItemsZonas")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult> GetReporteItemZonaExcel()
        {
            try
            {
                List<ReporteItemMedidoZonaDTO> listaReporte = new List<ReporteItemMedidoZonaDTO>();

                List<Contrato> contratos = await repositorio.GetContratosAndItems();

                ReporteItemMedidoZonaDTO repTitulo = new ReporteItemMedidoZonaDTO();
                repTitulo.Fecha = null;
                int flag = 1;
                foreach (var c in contratos.OrderBy(x => x.Zona.CodigoZona)) //CARGO ZONAS
                {
                    switch (flag)
                    {
                        case 1:
                            repTitulo.Zona1 = c.Zona.CodigoZona;
                            break;
                        case 2:
                            repTitulo.Zona2 = c.Zona.CodigoZona;
                            break;
                        case 3:
                            repTitulo.Zona3 = c.Zona.CodigoZona;
                            break;
                        case 4:
                            repTitulo.Zona4 = c.Zona.CodigoZona;
                            break;
                        case 5:
                            repTitulo.Zona5 = c.Zona.CodigoZona;
                            break;
                        case 6:
                            repTitulo.Zona6 = c.Zona.CodigoZona;
                            break;
                    }
                    flag++;
                }
                listaReporte.Add(repTitulo);

                foreach (var ci in contratos.First().ContratoItems.OrderBy(x => x.CodItem)) //CARGO ITEMS
                {
                    ReporteItemMedidoZonaDTO rep = new ReporteItemMedidoZonaDTO();
                    rep.Fecha = DateOnly.FromDateTime(DateTime.Now);
                    if (ci.Item.Unidad.CodUnidad != null)
                        rep.Unidad = ci.Item.Unidad.CodUnidad;
                    rep.Codigo = ci.CodItem;
                    rep.Item = ci.DescItem;
                    listaReporte.Add(rep);
                }

                foreach (var c in contratos.OrderBy(x => x.Zona.CodigoZona))
                {
                    var CertDefs = await certDefsRepositorio.GetCertificadosDefByIdContrato(c.Id);
                    foreach (var def in CertDefs)
                    {
                        foreach (var rep in listaReporte)
                        {
                            if (rep.Codigo == def.CodItem)          //CARGO VALORES
                            {
                                if (repTitulo.Zona1 == c.Zona.CodigoZona)
                                    rep.Zona1 = def.CantidadTotalEjecutado.ToString("0.00");
                                if (repTitulo.Zona2 == c.Zona.CodigoZona)
                                    rep.Zona2 = def.CantidadTotalEjecutado.ToString("0.00");
                                if (repTitulo.Zona3 == c.Zona.CodigoZona)
                                    rep.Zona3 = def.CantidadTotalEjecutado.ToString("0.00");
                                if (repTitulo.Zona4 == c.Zona.CodigoZona)
                                    rep.Zona4 = def.CantidadTotalEjecutado.ToString("0.00");
                                if (repTitulo.Zona5 == c.Zona.CodigoZona)
                                    rep.Zona5 = def.CantidadTotalEjecutado.ToString("0.00");
                                if (repTitulo.Zona6 == c.Zona.CodigoZona)
                                    rep.Zona6 = def.CantidadTotalEjecutado.ToString("0.00");
                            }
                        }
                    }
                }

                var filename = "Item Medidos Por Zona_"
                               + DateTime.Now.ToString("yyyyMMdd HHmm") + ".xlsx";

                var exportbytes = ExportToExcel<ReporteItemMedidoZonaDTO>(listaReporte, filename);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Reportes/Excel/{contratoId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult> GetReporteContratoExcel(int contratoId)
        {
            try
            {
                List<ReporteContratoDTO> listaReporte = new List<ReporteContratoDTO>();

                Contrato contrato = await repositorio.GetContratoByIdNoFull(contratoId);
                List<ContratoItem> contratoItemList = await contratoItemsRepositorio.GetContratoItemsByContratoId(contratoId);
                if (contrato == null) return NotFound("El Contrato indicado no existe.");

                List<Certificado> certificados = await certificadoRepositorio.GetReporteCertificadosByContrato(contratoId);
                if (certificados == null) return NotFound("No existen Certificados para el Contrato indicado.");

                foreach (ContratoItem item in contratoItemList)
                {
                    ReporteContratoDTO reporte = new ReporteContratoDTO();
                    reporte.Expediente = contrato.NumExpediente;
                    reporte.Zona = contrato.Caratula.Substring(0, 2);
                    reporte.Empresa = contrato.Empresa.Nombre;
                    reporte.Fecha = DateOnly.FromDateTime(item.FechaCrea);
                    reporte.ItemCantInformada = 0;
                    reporte.ItemCantMedida = 0;
                    reporte.ItemCodigo = item.CodItem;
                    reporte.ItemDescripcion = item.DescItem;
                    if (item.Item.Unidad.CodUnidad != null)
                    {
                        reporte.ItemUnidad = item.Item.Unidad.CodUnidad;
                    }
                    reporte.ItemCantTotal = item.CantidadTotal;

                    foreach (Certificado certificado in certificados)
                    {
                        foreach (CertificadoItem ci in certificado.CertificadoItems.OrderBy(x => x.CodItem))
                        {
                            if (item.Id == ci.ItemContratoId)
                            {
                                reporte.ItemCantMedida += ci.CantidadMedida;
                                reporte.ItemCantInformada += ci.CantidadInformada;
                            }
                        }
                    }
                    listaReporte.Add(reporte);
                }

                //Contrato_ZonaCod_Fecha
                var filename = "Contrato Provisorio_"
                               + contrato.Caratula.Substring(0, 2) + "_"
                               + DateTime.Now.ToString("yyyyMMdd HHmm") + ".xlsx"; //Sergio

                var exportbytes = ExportToExcel<ReporteContratoDTO>(listaReporte, filename);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Reportes/Excel/Periodos/{contratoId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult> GetReporteContratoExcelPorPeriodo(int contratoId)
        {
            try
            {
                List<ReporteItemMedidoContratoDTO> listaReporte = new List<ReporteItemMedidoContratoDTO>();

                Contrato contrato = await repositorio.GetContratoByIdNoFull(contratoId);
                List<ContratoItem> contratoItemList = await contratoItemsRepositorio.GetContratoItemsByContratoId(contratoId);
                if (contrato == null) return NotFound("El Contrato indicado no existe.");

                List<Certificado> certificados = await certificadoRepositorio.GetReporteCertificadosByContrato(contratoId);
                if (certificados == null) return NotFound("No existen Certificados para el Contrato indicado.");

                List<CertificadoItemDef> certDefs = await certDefsRepositorio.GetCertificadosDefByIdContrato(contratoId);
                if (certDefs == null) return NotFound("No existen Certificados Definitivos para el Contrato indicado.");


                int flag = 1;
                ReporteItemMedidoContratoDTO repTituloPeriodo = new ReporteItemMedidoContratoDTO();
                repTituloPeriodo.Fecha = null;
                foreach (Certificado c in certificados)
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

                foreach (ContratoItem item in contratoItemList.OrderBy(x => x.CodItem))
                {
                    ReporteItemMedidoContratoDTO reporte = new ReporteItemMedidoContratoDTO();
                    reporte.Fecha = DateOnly.FromDateTime(DateTime.Now);
                    reporte.Contrato = contrato.NumExpediente + " " + contrato.Caratula;
                    if (item.Item.Unidad.CodUnidad != null)
                        reporte.Unidad = item.Item.Unidad.CodUnidad;
                    reporte.Codigo = item.CodItem;
                    reporte.Item = item.DescItem;

                    foreach (CertificadoItemDef cdef in certDefs)
                    {
                        if (item.Id == cdef.ItemContratoId)
                        {
                            Certificado c = await certificadoRepositorio.GetCertificadoById(cdef.CertificadoId);
                            #region IFS carga periodos donde corresponde
                            if (repTituloPeriodo.Periodo1 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo1) + cdef.CantidadAcumulada;
                                reporte.Periodo1 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo2 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo2) + cdef.CantidadAcumulada;
                                reporte.Periodo2 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo3 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo3) + cdef.CantidadAcumulada;
                                reporte.Periodo3 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo4 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo4) + cdef.CantidadAcumulada;
                                reporte.Periodo4 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo5 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo5) + cdef.CantidadAcumulada;
                                reporte.Periodo5 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo6 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo6) + cdef.CantidadAcumulada;
                                reporte.Periodo6 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo7 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo7) + cdef.CantidadAcumulada;
                                reporte.Periodo7 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo8 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo8) + cdef.CantidadAcumulada;
                                reporte.Periodo8 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo9 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo9) + cdef.CantidadAcumulada;
                                reporte.Periodo9 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo10 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo10) + cdef.CantidadAcumulada;
                                reporte.Periodo10 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo11 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo11) + cdef.CantidadAcumulada;
                                reporte.Periodo11 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo12 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo12) + cdef.CantidadAcumulada;
                                reporte.Periodo12 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo13 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo13) + cdef.CantidadAcumulada;
                                reporte.Periodo13 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo14 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo14) + cdef.CantidadAcumulada;
                                reporte.Periodo14 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo15 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo15) + cdef.CantidadAcumulada;
                                reporte.Periodo15 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo16 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo16) + cdef.CantidadAcumulada;
                                reporte.Periodo16 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo17 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo17) + cdef.CantidadAcumulada;
                                reporte.Periodo17 = res.ToString("0.00");
                            }
                            else if (repTituloPeriodo.Periodo18 == c.Periodo)
                            {
                                decimal res = Convert.ToDecimal(reporte.Periodo18) + cdef.CantidadAcumulada;
                                reporte.Periodo18 = res.ToString("0.00");
                            }
                            #endregion
                        }
                    }
                    listaReporte.Add(reporte);
                }

                //Contrato_ZonaCod_Fecha
                var filename = "Items Medidos De Contrato_"
                               + contrato.Caratula.Substring(0, 2) + "_"
                               + DateTime.Now.ToString("yyyyMMdd HHmm") + ".xlsx";

                var exportbytes = ExportToExcel<ReporteItemMedidoContratoDTO>(listaReporte, filename);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos")]
        public async Task<ActionResult<int>> Post(ContratoDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                Contrato entidad = mapper.Map<Contrato>(dto);

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<Contrato>(entidad);
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
        public async Task<ActionResult<bool>> Put(ContratoDTO dto, int id)
        {
            try
            {
                dto.Certificados = null;
                dto.ContratoItems = null;
                dto.Eventos = null;
                dto.Empresa = null;
                dto.Zona = null;
                Contrato entidad = mapper.Map<Contrato>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<Contrato>(entidad);
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
                    return Ok($"El Contrato ha sido dado de baja del sistema");
                }
                return NotFound("El Contrato a dar de baja no a sido encontrado");
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
        //            return Ok($"El Contrato ha sido borrado");
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
