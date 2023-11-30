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
    [Route("GOP/api/Eventoparams")]
    public class EventoParamsController : GOPBaseController
    {
        private readonly IEventoParamsRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly UserManager<GOPUser> userManager;

        public EventoParamsController(IEventoParamsRepositorio repositorio, 
                                      IMapper mapper,
                                      UserManager<GOPUser> userManager)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EventoParamDTO>>> GetFull()
        {
            try
            {
                List<EventoParam> lista = await repositorio.GetActivos();

                List<EventoParamDTO> result = mapper.Map<List<EventoParamDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getEventoparams")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EventoParamDTO>>> GetEventoParams() //CON INCLUDE
        {
            try
            {
                List<EventoParam> lista = await repositorio.GetEventoParams();

                List<EventoParamDTO> result = mapper.Map<List<EventoParamDTO>>(lista);
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
        public async Task<ActionResult<EventoParamDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetEventoParamById(id);
                if (res == null) return NotFound();

                EventoParamDTO result = mapper.Map<EventoParamDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpGet("getactivos")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        //Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        //public async Task<ActionResult<List<EventoParamDTO>>> GetActivos()
        //{
        //    try
        //    {
        //        List<EventoParam> lista = await repositorio.GetActivos();
        //        List<EventoParamDTO> result = mapper.Map<List<EventoParamDTO>>(lista);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente")]
        public async Task<ActionResult<int>> Post(EventoParamDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                # region Control de autorizacion
                string UserRol = ObtenerRol();
                Persona persona = ObtenerPersona<EventoParam>(repositorio, userManager);

                if (dto.Evento.ZonaId != null && UserRol == "Zona2")
                {
                    if (!EsProfesionalDeZona<EventoParam>(repositorio,
                                                          persona.Id,
                                                          (int)dto.Evento.ZonaId))
                    {

                        return BadRequest("Grabación no autorizada");
                    }
                }

                if (dto.Evento.FrenteObraId != null && UserRol == "Frente")
                {
                    if (!EsProfesionalDeFrente<EventoParam>(repositorio,
                                                            persona.Id,
                                                            (int)dto.Evento.FrenteObraId))
                    {
                        return BadRequest("Grabación no autorizada");
                    }
                }
                #endregion

                EventoParam entidad = mapper.Map<EventoParam>(dto);

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<EventoParam>(entidad);
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
        public async Task<ActionResult<bool>> Put(EventoParamDTO dto, int id)
        {
            try
            {
                # region Control de autorizacion
                string UserRol = ObtenerRol();
                Persona persona = ObtenerPersona<EventoParam>(repositorio, userManager);
                if (dto.Evento.ZonaId != null && UserRol == "Zona2")
                {
                    if (!EsProfesionalDeZona<EventoParam>(repositorio,
                                                          persona.Id,
                                                          (int)dto.Evento.ZonaId))
                    {

                        return BadRequest("Grabación no autorizada");
                    }
                }

                if (dto.Evento.FrenteObraId != null && UserRol == "Frente")
                {
                    if (!EsProfesionalDeFrente<EventoParam>(repositorio,
                                                            persona.Id,
                                                            (int)dto.Evento.FrenteObraId))
                    {
                        return BadRequest("Grabación no autorizada");
                    }
                }
                #endregion

                dto.Evento = null;
                dto.Unidad = null;
                EventoParam entidad = mapper.Map<EventoParam>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<EventoParam>(entidad);
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
                if (await repositorio.Baja(id, ObtenerUserId()))
                {
                    return Ok($"El Item ha sido dada de baja del sistema");
                }
                return NotFound("El Item a dar de baja no a sido encontrado");
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
        //            return Ok($"El Item ha sido borrado");
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
