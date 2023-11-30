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
    [Route("GOP/api/empresas")]
    public class EmpresasController : GOPBaseController
    {
        private readonly IEmpresasRepositorio repositorio;
        private readonly IMapper mapper;

        public EmpresasController(IEmpresasRepositorio repositorio, 
                                  IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EmpresaDTO>>> GetFull()
        {
            try
            {
                List<Empresa> lista = await repositorio.GetActivos();

                List<EmpresaDTO> result = mapper.Map<List<EmpresaDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getempresas")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EmpresaDTO>>> GetEmpresas() //CON INCLUDE
        {
            try
            {
                List<Empresa> lista = await repositorio.GetEmpresas();

                List<EmpresaDTO> result = mapper.Map<List<EmpresaDTO>>(lista);
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
        public async Task<ActionResult<EmpresaDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetEmpresaById(id);
                if (res == null) return NotFound();

                EmpresaDTO result = mapper.Map<EmpresaDTO>(res);
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
        public async Task<ActionResult<int>> Post(EmpresaDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                Empresa entidad = mapper.Map<Empresa>(dto);

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<Empresa>(entidad);
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
        public async Task<ActionResult<bool>> Put(EmpresaDTO dto, int id)
        {
            try
            {
                dto.Eventos = null;
                dto.ContratoObras = null;
                dto.Profesionales = null;
                Empresa entidad = mapper.Map<Empresa>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<Empresa>(entidad);
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
                    return Ok($"La Empresa ha sido dada de baja del sistema");
                }
                return NotFound("La Empresa a dar de baja no a sido encontrado");
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
        //            return Ok($"La Empresa ha sido borrada");
        //        }
        //        return NotFound("La Empresa que desea borrar no ha sido encontrado");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
