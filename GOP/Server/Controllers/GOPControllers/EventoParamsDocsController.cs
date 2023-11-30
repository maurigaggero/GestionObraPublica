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
    [Route("GOP/api/EventoParamsdocs")]
    public class EventoParamDocsController : GOPBaseController
    {
        private readonly IEventoParamDocsRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly UserManager<GOPUser> userManager;

        public EventoParamDocsController(IEventoParamDocsRepositorio repositorio, 
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
        public async Task<ActionResult<List<EventoParamDocDTO>>> GetFull()
        {
            try
            {
                List<EventoParamDoc> lista = await repositorio.GetActivos();
                List<EventoParamDocDTO> result = mapper.Map<List<EventoParamDocDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getEventoParamdocs")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EventoParamDocDTO>>> GetEventoParamsDocs() //CON INCLUDE
        {
            try
            {
                List<EventoParamDoc> lista = await repositorio.GetEventoParamDocs();
                List<EventoParamDocDTO> result = mapper.Map<List<EventoParamDocDTO>>(lista);
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
        public async Task<ActionResult<EventoParamDocDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetEventoParamDocById(id);
                if (res == null) return NotFound();

                EventoParamDocDTO result = mapper.Map<EventoParamDocDTO>(res);

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
        public async Task<ActionResult<int>> Post(EventoParamDocDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                # region Control de autorizacion
                string UserRol = ObtenerRol();
                Persona persona = ObtenerPersona<EventoParamDoc>(repositorio, userManager);

                if (UserRol == "Consulta1"
                    || UserRol == "Consulta2")
                {
                    return BadRequest("Grabación no autorizada");
                }
                //FALTA: controlar autorizacion por zona
                #endregion

                EventoParamDoc entidad = mapper.Map<EventoParamDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.GuardarArchivo(archivo, dto.Extension, "EventoParametros");
                }

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<EventoParamDoc>(entidad);
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
        public async Task<ActionResult<bool>> Put(EventoParamDocDTO dto, int id)
        {
            try
            {
                # region Control de autorizacion
                string UserRol = ObtenerRol();
                Persona persona = ObtenerPersona<EventoParamDoc>(repositorio, userManager);

                if (UserRol == "Consulta1"
                    || UserRol == "Consulta2")
                {
                    return BadRequest("Grabación no autorizada");
                }
                //FALTA: controlar autorizacion por zona
                #endregion

                dto.EventoParam = null;
                EventoParamDoc entidad = mapper.Map<EventoParamDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.EditarArchivo(archivo, dto.Extension, "EventoParametros", entidad.PathDoc);
                }

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<EventoParamDoc>(entidad);
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
        //        EventoParamDocDTO item = Get(id).Result.Value;
        //        await almacenadorArchivos.EliminarArchivo(item.PathDoc);
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

