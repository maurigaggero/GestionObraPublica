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
    [Route("GOP/api/Calle")]
    public class CalleController : GOPBaseController
    {
        private readonly ICalleRepositorio repositorio;
        private readonly IMapper mapper;

        public CalleController(ICalleRepositorio repositorio, 
                               IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<CalleDTO>>> GetFull()
        {
            try
            {
                List<Calle> lista = await repositorio.GetActivos();

                List<CalleDTO> result = mapper.Map<List<CalleDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
               Roles = "Admin, BaseDatos")]
        public async Task<ActionResult<CalleDTO>> Get(int id)
        {
            try
            {
                Calle calle = await repositorio.GetById(id);

                CalleDTO result = mapper.Map<CalleDTO>(calle);
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
        public async Task<ActionResult<int>> Post(CalleDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                Calle entidad = mapper.Map<Calle>(dto);

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<Calle>(entidad);
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
        public async Task<ActionResult<bool>> Put(CalleDTO dto, int id)
        {
            try
            {
                Calle entidad = mapper.Map<Calle>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<Calle>(entidad);
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
                    return Ok($"El Calle ha sido dado de baja del sistema");
                }
                return NotFound("El Calle a dar de baja no a sido encontrado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpDelete("{id:int}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        //       Roles = "Admin, BaseDatos")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        if (await repositorio.Delete(id))
        //        {
        //            return Ok($"El Calle ha sido borrado");
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
