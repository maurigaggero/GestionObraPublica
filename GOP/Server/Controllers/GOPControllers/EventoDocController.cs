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
    [Route("GOP/api/EventoDoc")]
    public class EventoDocController : GOPBaseController
    {
        private readonly IEventoDocRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly UserManager<GOPUser> userManager;

        public EventoDocController(IEventoDocRepositorio repositorio, 
                                   IMapper mapper, 
                                   IAlmacenadorArchivos almacenadorArchivos,
                                   UserManager<GOPUser> userManager)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
            this.userManager = userManager;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EventoDocDTO>>> GetFull()
        {
            try
            {
                List<EventoDoc> lista = await repositorio.GetActivos();

                List<EventoDocDTO> result = mapper.Map<List<EventoDocDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getEventoDoc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EventoDocDTO>>> GetEventoDoc() //CON INCLUDE
        {
            try
            {
                List<EventoDoc> lista = await repositorio.GetEventoDocs();

                List<EventoDocDTO> result = mapper.Map<List<EventoDocDTO>>(lista);
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
        public async Task<ActionResult<EventoDocDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetEventoDocById(id);
                if (res == null) return NotFound();

                EventoDocDTO result = mapper.Map<EventoDocDTO>(res);
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
        public async Task<ActionResult<int>> Post(EventoDocDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                # region Control de autorizacion
                //string UserRol = ObtenerRol();
                //Persona persona = ObtenerPersona<EventoDoc>(repositorio, userManager);

                //if (dto.Evento.ZonaId != null && UserRol == "Zona2")
                //{
                //    if (!EsProfesionalDeZona<EventoDoc>(repositorio,
                //                                        persona.Id,
                //                                        (int)dto.Evento.ZonaId))
                //    {

                //        return BadRequest("Grabación no autorizada");
                //    }
                //}

                //if (dto.Evento.FrenteObraId != null && UserRol == "Frente")
                //{
                //    if (!EsProfesionalDeFrente<EventoDoc>(repositorio,
                //                                          persona.Id,
                //                                          (int)dto.Evento.FrenteObraId))
                //    {
                //        return BadRequest("Grabación no autorizada");
                //    }
                //}
                #endregion

                EventoDoc entidad = mapper.Map<EventoDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.GuardarArchivo(archivo, dto.Extension, "Eventos");
                }

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<EventoDoc>(entidad);
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
        public async Task<ActionResult<bool>> Put(EventoDocDTO dto, int id)
        {
            try
            {
                # region Control de autorizacion
                string UserRol = ObtenerRol();
                Persona persona = ObtenerPersona<EventoDoc>(repositorio, userManager);

                if (dto.Evento.ZonaId != null && UserRol == "Zona2")
                {
                    if (!EsProfesionalDeZona<EventoDoc>(repositorio,
                                                        persona.Id,
                                                        (int)dto.Evento.ZonaId))
                    {

                        return BadRequest("Grabación no autorizada");
                    }
                }

                if (dto.Evento.FrenteObraId != null && UserRol == "Frente")
                {
                    if (!EsProfesionalDeFrente<EventoDoc>(repositorio,
                                                          persona.Id,
                                                          (int)dto.Evento.FrenteObraId))
                    {
                        return BadRequest("Grabación no autorizada");
                    }
                }
                #endregion

                dto.Evento = null;
                EventoDoc entidad = mapper.Map<EventoDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.GuardarArchivo(archivo, dto.Extension, "Eventos");
                }

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<EventoDoc>(entidad);
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
                //EventoDocDTO doc = Get(id).Result.Value;
                //await almacenadorArchivos.EliminarArchivo(doc.PathDoc);

                if (await repositorio.Baja(id, ObtenerUserId()))
                {
                    return Ok($"El EventoDoc ha sido dado de baja del sistema");
                }
                return NotFound("El EventoDoc a dar de baja no a sido encontrado");
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
        //        EventoDocDTO doc = Get(id).Result.Value;
        //        await almacenadorArchivos.EliminarArchivo(doc.PathDoc);

        //        if (await repositorio.Delete(id))
        //        {
        //            return Ok($"El EventoDoc ha sido borrado");
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
