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
    [Route("GOP/api/empprofesionales")]
    public class EmpresaProfesionalesController : GOPBaseController
    {
        private readonly IEmpresaProfesionalesRepositorio repositorio;
        private readonly IMapper mapper;

        public EmpresaProfesionalesController(IEmpresaProfesionalesRepositorio repositorio, 
                                              IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EmpresaProfesionalDTO>>> GetFull()
        {
            try
            {
                List<EmpresaProfesional> lista = await repositorio.GetActivos();

                List<EmpresaProfesionalDTO> result = mapper.Map<List<EmpresaProfesionalDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getempresaprofesionals/{empresaId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EmpresaProfesionalDTO>>> GetEmpresaProfesionalProfesionales(int empresaId) //CON INCLUDE
        {
            try
            {
                List<EmpresaProfesional> lista = await repositorio.GetEmpProfesionales(empresaId);

                List<EmpresaProfesionalDTO> result = mapper.Map<List<EmpresaProfesionalDTO>>(lista);
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
        public async Task<ActionResult<EmpresaProfesionalDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetEmpProfesionalfById(id);
                if (res == null) return NotFound();

                EmpresaProfesionalDTO result = mapper.Map<EmpresaProfesionalDTO>(res);
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
        public async Task<ActionResult<int>> Post(EmpresaProfesionalDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                EmpresaProfesional entidad = mapper.Map<EmpresaProfesional>(dto);

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<EmpresaProfesional>(entidad);
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
        public async Task<ActionResult<bool>> Put(EmpresaProfesionalDTO dto, int id)
        {
            try
            {
                dto.Persona = null;
                dto.Empresa = null;
                EmpresaProfesional entidad = mapper.Map<EmpresaProfesional>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<EmpresaProfesional>(entidad);
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
                    return Ok($"El Profesional de la Empresa ha sido dada de baja del sistema");
                }
                return NotFound("El Profesional de la Empresa a dar de baja no ha sido encontrado");
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
        //            return Ok($"El Profesional de la Empresa ha sido borrado");
        //        }
        //        return NotFound("El Profesional de la Empresa que desea borrar no ha sido encontrado");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
