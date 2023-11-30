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
    [Route("GOP/api/zonasprofesionales")]
    public class ZonaProfesionalesController : GOPBaseController
    {
        private readonly IZonasProfesionalesRepositorio repositorio;
        private readonly IMapper mapper;

        public ZonaProfesionalesController(IZonasProfesionalesRepositorio repositorio, 
                                           IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ZonaProfesionalDTO>>> GetFull()
        {
            try
            {
                List<ZonaProfesional> lista = await repositorio.GetActivos();

                List<ZonaProfesionalDTO> result = mapper.Map<List<ZonaProfesionalDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getZonaProfesionals/{zonaId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ZonaProfesionalDTO>>> GetZonaProfesionalProfesionales(int zonaId) //CON INCLUDE
        {
            try
            {
                List<ZonaProfesional> lista = await repositorio.GetZonasProfesionales(zonaId);

                List<ZonaProfesionalDTO> result = mapper.Map<List<ZonaProfesionalDTO>>(lista);
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
        public async Task<ActionResult<ZonaProfesionalDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetZonaProfesionalfById(id);
                if (res == null) return NotFound();

                ZonaProfesionalDTO result = mapper.Map<ZonaProfesionalDTO>(res);
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
        public async Task<ActionResult<int>> Post(ZonaProfesionalDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                ZonaProfesional entidad = mapper.Map<ZonaProfesional>(dto);

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<ZonaProfesional>(entidad);
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
        public async Task<ActionResult<bool>> Put(ZonaProfesionalDTO dto, int id)
        {
            try
            {
                dto.Persona = null;
                dto.Zona = null;
                ZonaProfesional entidad = mapper.Map<ZonaProfesional>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<ZonaProfesional>(entidad);
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
                    return Ok($"El Profesional de la Zona ha sido dada de baja del sistema");
                }
                return NotFound("El Profesional de la Zona a dar de baja no ha sido encontrado");
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
        //            return Ok($"El Profesional de la Zona ha sido borrado");
        //        }
        //        return NotFound("El Profesional de la Zona que desea borrar no ha sido encontrado");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
