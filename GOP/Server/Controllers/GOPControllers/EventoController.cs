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
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace GOP.Server.Controllers.GOPControllers
{
    [ApiController]
    [Route("GOP/api/Evento")]
    public class EventoController : GOPBaseController
    {
        private readonly IEventoRepositorio repositorio;
        private readonly ICertificadoRepositorio certificadoRepositorio;
        private readonly IFrenteObrasRepositorio frenteObrasRepositorio;
        private readonly IZonasRepositorio zonaRepositorio;
        private readonly IContratoRepositorio contratoRepositorio;
        private readonly IMapper mapper;
        private readonly UserManager<GOPUser> userManager;

        public EventoController(IEventoRepositorio repositorio, 
                                ICertificadoRepositorio certificadoRepositorio,
                                IFrenteObrasRepositorio frenteObrasRepositorio,
                                IContratoRepositorio contratoRepositorio,
                                IZonasRepositorio zonaRepositorio,
                                IMapper mapper,
                                UserManager<GOPUser> userManager)
        {
            this.repositorio = repositorio;
            this.certificadoRepositorio = certificadoRepositorio;
            this.frenteObrasRepositorio = frenteObrasRepositorio;
            this.zonaRepositorio = zonaRepositorio;
            this.contratoRepositorio = contratoRepositorio;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EventoDTO>>> GetFull()
        {
            try
            {
                List<Evento> lista = await repositorio.GetActivos();

                List<EventoDTO> result = mapper.Map<List<EventoDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getEvento")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EventoDTO>>> GetEvento([FromQuery] FiltroEventoDTO filtro) //CON INCLUDE
        {
            try
            {
                List<Evento> lista = await repositorio.GetEventos(filtro);

                HttpContext.Response.Headers.Add("Registros", lista.Count.ToString());

                string UserRol = ObtenerRol();

                if (UserRol == "HyS")
                {
                    lista = lista.Where(x => x.Tipo.CodTipo.Contains("HYS")).ToList();
                }
                else if (UserRol == "Zona2" 
                         || UserRol == "Frente" 
                         || UserRol == "Consulta1")
                {
                    lista = lista.Where(x => !x.Tipo.CodTipo.Contains("HYS")).ToList();
                }
                //else if (UserRol == "Zona1" || UserRol == "Zona2" ||
                //        UserRol == "Frente" || UserRol == "Consulta1")
                //{

                    List<EventoDTO> result = mapper.Map<List<EventoDTO>>(lista);

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
        public async Task<ActionResult<List<EventoDTO>>> GetReportesEventos([FromQuery] FiltroEventoReporteDTO filtro) //CON INCLUDE
        {
            try
            {
                List<Evento> lista = await repositorio.GetReporteEventos(filtro);

                HttpContext.Response.Headers.Add("Registros", lista.Count.ToString());

                string UserRol = ObtenerRol();

                if (UserRol == "HyS")
                {
                    lista = lista.Where(x => x.Tipo.CodTipo.Contains("HYS")).ToList();
                }
                else if (UserRol == "Zona2" 
                         || UserRol == "Frente" 
                         || UserRol == "Consulta1")
                {
                    lista = lista.Where(x => !x.Tipo.CodTipo.Contains("HYS")).ToList();
                }

                //else if (UserRol == "Zona1" || UserRol == "Zona2" ||
                //        UserRol == "Frente" || UserRol == "Consulta1")
                //{
                //    lista = lista.Where(x => !x.Tipo.CodTipo.Contains("HYS")).ToList();
                //}

                List<EventoDTO> result = mapper.Map<List<EventoDTO>>(lista);

                foreach (var item in result)
                {
                    if (item.CertificadoId != null && item.CertificadoId!=0)
                    {
                        item.Certificado = mapper.Map<CertificadoDTO>( 
                            await certificadoRepositorio.GetCertificadoByIdNoFull((int)item.CertificadoId));
                    }
                    if (item.FrenteObraId != null && item.FrenteObraId != 0)
                    {
                        item.FrenteObra = mapper.Map<FrenteObraDTO>(
                            await frenteObrasRepositorio.GetFrenteObraByIdNoFull((int)item.FrenteObraId));
                    }
                    if (item.ContratoId != null && item.ContratoId != 0)
                    {
                        item.Contrato = mapper.Map<ContratoDTO>(
                            await contratoRepositorio.GetContratoByIdNoFull((int)item.ContratoId));
                    }
                    if (item.ZonaId != null && item.ZonaId != 0)
                    {
                        item.Zona = mapper.Map<ZonaDTO>(
                            await zonaRepositorio.GetZonaByIdNoFull((int)item.ZonaId));
                    }
                }

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
        public async Task<ActionResult<EventoDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetEventoById(id);
                if (res == null) return NotFound();

                EventoDTO result = mapper.Map<EventoDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente")]
        public async Task<ActionResult<int>> Post(EventoDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");
                dto.Numero = null;

                Evento entidad = mapper.Map<Evento>(dto);

                # region Control de autorizacion
                string UserRol = ObtenerRol();
                Persona persona = ObtenerPersona<Evento>(repositorio, userManager);
                
                if (dto.ZonaId != null && UserRol == "Zona2")
                {
                    if (!EsProfesionalDeZona<Evento>(repositorio,
                                                     persona.Id,
                                                     (int)dto.ZonaId))
                    {

                        return Unauthorized("Grabación no autorizada. El Usuario no es profesional de la Zona seleccionada");
                    }
                }
                
                if (dto.FrenteObraId != null && UserRol == "Frente")
                {
                    if (!EsProfesionalDeFrente<Evento>(repositorio,
                                                       persona.Id,
                                                       (int)dto.FrenteObraId))
                    {
                        return Unauthorized("Grabación no autorizada. El Usuario no es profesional del Frente seleccionado");
                    }
                }
                #endregion

                int numeroEvento = await repositorio.CantidadEventos() + 1;
                var tipoEvento = await repositorio.GetTipoEvento(entidad.EventoTipoId);
                entidad.Numero = tipoEvento.CodTipo + " - " + numeroEvento.ToString("D6");

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<Evento>(entidad);
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
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente")]
        public async Task<ActionResult<bool>> Put(EventoDTO dto, int id)
        {
            try
            {
                # region Control de autorizacion
                string UserRol = ObtenerRol();
                Persona persona = ObtenerPersona<Evento>(repositorio, userManager);

                if (dto.ZonaId != null && UserRol == "Zona2")
                {
                    if (!EsProfesionalDeZona<Evento>(repositorio,
                                                     persona.Id,
                                                     (int)dto.ZonaId))
                    {

                        return Unauthorized("Grabación no autorizada. El Usuario no es profesional de la Zona seleccionada");
                    }
                }

                if (dto.FrenteObraId != null && UserRol == "Frente")
                {
                    if (!EsProfesionalDeFrente<Evento>(repositorio,
                                                       persona.Id,
                                                       (int)dto.FrenteObraId))
                    {
                        return Unauthorized("Grabación no autorizada. El Usuario no es profesional del Frente seleccionado");
                    }
                }
                #endregion

                dto.Contrato = null;
                dto.Certificado = null;
                dto.Tipo = null;
                dto.Empresa = null;
                dto.Zona = null;
                dto.Documentos = null;
                dto.Prametros = null;
                Evento entidad = mapper.Map<Evento>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<Evento>(entidad);
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
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente")]
        public async Task<ActionResult> Baja(int id)
        {
            try
            {
                Evento entidad = await repositorio.GetEventoById(id);

                string UserRol = ObtenerRol();
                Persona persona = ObtenerPersona<Evento>(repositorio, userManager);
                if (entidad.ZonaId != null && UserRol == "Zona2")
                {
                    if (!EsProfesionalDeZona<Evento>(repositorio,
                                                     persona.Id,
                                                     (int)entidad.ZonaId))
                    {
                        return BadRequest("Modificación no autorizada");
                    }
                }
                if (entidad.FrenteObraId != null && UserRol == "Frente")
                {
                    if (!EsProfesionalDeFrente<Evento>(repositorio,
                                                       persona.Id,
                                                       (int)entidad.FrenteObraId))
                    {
                        return BadRequest("Modificación no autorizada");
                    }
                }

                if (await repositorio.Baja(id, ObtenerUserId()))
                {
                    return Ok($"El Evento ha sido dado de baja del sistema");
                }
                return NotFound("El Evento a dar de baja no a sido encontrado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpDelete("{id:int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        //Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        if (await repositorio.Delete(id))
        //        {
        //            return Ok($"El Evento ha sido borrado");
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
