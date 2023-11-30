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
    [Route("GOP/api/frenteobras")]
    public class FrenteObrasController : GOPBaseController
    {
        private readonly IFrenteObrasRepositorio repositorio;
        private readonly IMapper mapper;

        public FrenteObrasController(IFrenteObrasRepositorio repositorio, 
                                     IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<FrenteObraDTO>>> GetFull()
        {
            try
            {
                List<FrenteObra> lista = await repositorio.GetActivos();

                List<FrenteObraDTO> result = mapper.Map<List<FrenteObraDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getFrenteObras")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<FrenteObraDTO>>> GetFrenteObras() //CON INCLUDE
        {
            try
            {
                List<FrenteObra> lista = await repositorio.GetFrenteObras();

                List<FrenteObraDTO> result = mapper.Map<List<FrenteObraDTO>>(lista);
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
        public async Task<ActionResult<FrenteObraDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetFrenteObraById(id);
                if (res == null) return NotFound();

                FrenteObraDTO result = mapper.Map<FrenteObraDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getFrenteObrasPorPersona")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<FrenteObraDTO>>> GetFrenteObrasPorPersona(int idPersona)
        {
            try
            {
                List<FrenteObra> lista = await repositorio.GetFrenteObrasPorPersona(idPersona);

                List<FrenteObraDTO> result = mapper.Map<List<FrenteObraDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getFrenteObrasPorZona/{idZona:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<FrenteObraDTO>>> GetFrenteObrasPorZona(int idZona)
        {
            try
            {
                List<FrenteObra> lista = await repositorio.GetFrenteObrasPorZona(idZona);

                List<FrenteObraDTO> result = mapper.Map<List<FrenteObraDTO>>(lista);
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
        public async Task<ActionResult<int>> Post(FrenteObraDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                FrenteObra entidad = mapper.Map<FrenteObra>(dto);

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<FrenteObra>(entidad);
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
        public async Task<ActionResult<bool>> Put(FrenteObraDTO dto, int id)
        {
            try
            {
                dto.Zona = null;
                dto.Profesionales = null;
                FrenteObra entidad = mapper.Map<FrenteObra>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<FrenteObra>(entidad);
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
                    return Ok($"El Frente Obra ha sido dada de baja del sistema");
                }
                return NotFound("El Frente Obra a dar de baja no a sido encontrado");
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
        //            return Ok($"El Frente Obra ha sido borrada");
        //        }
        //        return NotFound("El Frente Obra que desea borrar no ha sido encontrado");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
