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
    [Route("GOP/api/zonas")]
    public class ZonasController : GOPBaseController
    {
        private readonly IZonasRepositorio repositorio;
        private readonly IMapper mapper;

        public ZonasController(IZonasRepositorio repositorio, 
                               IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ZonaDTO>>> GetFull()
        {
            try
            {
                List<Zona> lista = await repositorio.GetActivos();

                List<ZonaDTO> result = mapper.Map<List<ZonaDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getZonas")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ZonaDTO>>> GetZonas() //CON INCLUDE
        {
            try
            {
                List<Zona> lista = await repositorio.GetZonas();

                List<ZonaDTO> result = mapper.Map<List<ZonaDTO>>(lista);
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
        public async Task<ActionResult<ZonaDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetZonaById(id);
                if (res == null) return NotFound();

                ZonaDTO result = mapper.Map<ZonaDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("getZonasPorPersona")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ZonaDTO>>> GetZonasPorPersona(int idPersona)
        {
            try
            {
                List<Zona> lista = await repositorio.GetZonasPorPersona(idPersona);

                List<ZonaDTO> result = mapper.Map<List<ZonaDTO>>(lista);
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
        public async Task<ActionResult<int>> Post(ZonaDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                Zona entidad = mapper.Map<Zona>(dto);

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<Zona>(entidad);
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
        public async Task<ActionResult<bool>> Put(ZonaDTO dto, int id)
        {
            try
            {
                dto.Profesionales = null;
                dto.Eventos = null;
                dto.Contratos = null;
                dto.FrenteObras = null;
                Zona entidad = mapper.Map<Zona>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<Zona>(entidad);
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
                    return Ok($"La Zona ha sido dada de baja del sistema");
                }
                return NotFound("La Zona a dar de baja no a sido encontrado");
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
        //            return Ok($"La Zona ha sido borrada");
        //        }
        //        return NotFound("La Zona que desea borrar no ha sido encontrado");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
