using AutoMapper;
using GOP.BD.Data;
using GOP.BD.Data.Entity;
using GOP.Repositorio.Repos;
using GOP.Shared.DTOs.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GOP.Server.Controllers.GOPControllers
{
    [ApiController]
    [Route("GOP/api/EventoTipo")]
    public class EventoTipoController : GOPBaseController
    {
        private readonly IEventoTipoRepositorio repositorio;
        private readonly IMapper mapper;

        public EventoTipoController(IEventoTipoRepositorio repositorio, 
                                    IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EventoTipoDTO>>> GetFull()
        {
            try
            {
                List<EventoTipo> lista = await repositorio.GetActivos();

                string UserRol = HttpContext.User.Claims
                                .Where(claim => claim.Type == ClaimTypes.Role)
                                .FirstOrDefault().Value;

                if (UserRol == "HyS")
                {
                    lista = lista.Where(x => x.CodTipo.Contains("HYS")).ToList();
                }
                else if (UserRol == "Zona2" 
                         || UserRol == "Frente" 
                         || UserRol == "Consulta1")
                {
                    lista = lista.Where(x => !x.CodTipo.Contains("HYS")).ToList();
                }
                //else if (UserRol == "Zona1" || UserRol == "Zona2" ||
                //        UserRol == "Frente" || UserRol == "Consulta1")
                //{
                //    lista = lista.Where(x => !x.CodTipo.Contains("HYS")).ToList();
                //}

                List<EventoTipoDTO> result = mapper.Map<List<EventoTipoDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getEventoTipo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EventoTipoDTO>>> GetEventoTipo() //CON INCLUDE
        {
            try
            {
                List<EventoTipo> lista = await repositorio.GetEventoTipos();

                string UserRol = HttpContext.User.Claims
                                .Where(claim => claim.Type == ClaimTypes.Role)
                                .FirstOrDefault().Value;

                if (UserRol == "HyS")
                {
                    lista = lista.Where(x => x.CodTipo.Contains("HYS")).ToList();
                }
                else if (UserRol == "Zona2" 
                         || UserRol == "Frente" 
                         || UserRol == "Consulta1")
                {
                    lista = lista.Where(x => !x.CodTipo.Contains("HYS")).ToList();
                }
                //else if (UserRol == "Zona1" || UserRol == "Zona2" ||
                //        UserRol == "Frente" || UserRol == "Consulta1")
                //{
                //    lista = lista.Where(x => !x.CodTipo.Contains("HYS")).ToList();
                //}

                List<EventoTipoDTO> result = mapper.Map<List<EventoTipoDTO>>(lista);
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
        public async Task<ActionResult<EventoTipoDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetEventoTipoById(id);
                if (res == null) return NotFound();

                EventoTipoDTO result = mapper.Map<EventoTipoDTO>(res);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos")]
        public async Task<ActionResult<int>> Post(EventoTipoDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                EventoTipo entidad = mapper.Map<EventoTipo>(dto);

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<EventoTipo>(entidad);
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
        public async Task<ActionResult<bool>> Put(EventoTipoDTO dto, int id)
        {
            try
            {
                dto.Eventos = null;
                EventoTipo entidad = mapper.Map<EventoTipo>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<EventoTipo>(entidad);
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
                    return Ok($"El EventoTipo ha sido dado de baja del sistema");
                }
                return NotFound("El EventoTipo a dar de baja no a sido encontrado");
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
        //            return Ok($"El EventoTipo ha sido borrado");
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
