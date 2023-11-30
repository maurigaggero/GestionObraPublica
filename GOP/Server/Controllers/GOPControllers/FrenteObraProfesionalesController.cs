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
    [Route("GOP/api/frenteobraprofesionales")]
    public class FrenteObraProfesionalesController : GOPBaseController
    {
        private readonly IFrenteObraProfesionalesRepositorio repositorio;
        private readonly IMapper mapper;

        public FrenteObraProfesionalesController(IFrenteObraProfesionalesRepositorio repositorio, 
                                                 IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<FrenteObraProfesionalDTO>>> GetFull()
        {
            try
            {
                List<FrenteObraProfesional> lista = await repositorio.GetActivos();

                List<FrenteObraProfesionalDTO> result = mapper.Map<List<FrenteObraProfesionalDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getFrenteObraProfesionals")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<FrenteObraProfesionalDTO>>> GetFrenteObraProfesional() //CON INCLUDE
        {
            try
            {
                List<FrenteObraProfesional> lista = await repositorio.GetFrenteObraProfesionales();

                List<FrenteObraProfesionalDTO> result = mapper.Map<List<FrenteObraProfesionalDTO>>(lista);
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
        public async Task<ActionResult<FrenteObraProfesionalDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetFrenteObraProfesionalfById(id);
                if (res == null) return NotFound();

                FrenteObraProfesionalDTO result = mapper.Map<FrenteObraProfesionalDTO>(res);
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
        public async Task<ActionResult<int>> Post(FrenteObraProfesionalDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                FrenteObraProfesional entidad = mapper.Map<FrenteObraProfesional>(dto);

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<FrenteObraProfesional>(entidad);
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
        public async Task<ActionResult<bool>> Put(FrenteObraProfesionalDTO dto, int id)
        {
            try
            {
                dto.Persona = null;
                dto.FrenteObra = null;
                FrenteObraProfesional entidad = mapper.Map<FrenteObraProfesional>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<FrenteObraProfesional>(entidad);
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
                    return Ok($"El Profesional del Frente de Obra ha sido dada de baja del sistema");
                }
                return NotFound("El Profesional del Frente de Obra a dar de baja no ha sido encontrado");
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
        //            return Ok($"El Profesional del Frente de Obra ha sido borrado");
        //        }
        //        return NotFound("El Profesional del Frente de Obra que desea borrar no ha sido encontrado");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
