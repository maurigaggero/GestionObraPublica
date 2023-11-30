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
    [Route("GOP/api/personas")]
    public class PersonasController : GOPBaseController
    {
        private readonly IPersonasRepositorio repositorio;
        private readonly IMapper mapper;

        public PersonasController(IPersonasRepositorio repositorio, 
                                  IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<PersonaDTO>>> GetFull()
        {
            try
            {
                List<Persona> lista = await repositorio.GetActivos();
                List<PersonaDTO> result = mapper.Map<List<PersonaDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getactivos")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<PersonaDTO>>> GetActivos()
        {
            try
            {
                List<Persona> lista = await repositorio.GetActivos();
                List<PersonaDTO> result = mapper.Map<List<PersonaDTO>>(lista);
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
        public async Task<ActionResult<PersonaDTO>> Get(int id)
        {
            try
            {
                var res = await repositorio.GetById(id);
                if (res == null) return NotFound();

                PersonaDTO result = mapper.Map<PersonaDTO>(res);
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
        public async Task<ActionResult<int>> Post(PersonaDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                Persona entidad = mapper.Map<Persona>(dto);

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<Persona>(entidad);
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
        public async Task<ActionResult<bool>> Put(PersonaDTO dto, int id)
        {
            try
            {
                Persona entidad = mapper.Map<Persona>(dto);

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<Persona>(entidad);
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
                    return Ok($"La persona ha sido dada de baja del sistema");
                }
                return NotFound("La persona a dar de baja no a sido encontrada");
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
        //            return Ok($"La persona ha sido borrada");
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
