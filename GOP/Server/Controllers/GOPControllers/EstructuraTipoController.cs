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
    [Route("GOP/api/EstructuraTipo")]
    public class EstructuraTipoController : GOPBaseController
    {
        private readonly IEstructuraTipoRepositorio repositorio;
        private readonly IMapper mapper;

        public EstructuraTipoController(IEstructuraTipoRepositorio repositorio, 
                                        IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EstructuraTipoDTO>>> GetFull()
        {
            try
            {
                List<EstructuraTipo> lista = await repositorio.GetActivos();

                List<EstructuraTipoDTO> result = mapper.Map<List<EstructuraTipoDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getEstructuraTipo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, HyS, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<EstructuraTipoDTO>>> GetEstructuraTipo() //CON INCLUDE
        {
            try
            {
                List<EstructuraTipo> lista = await repositorio.GetEstructuraTipos();

                List<EstructuraTipoDTO> result = mapper.Map<List<EstructuraTipoDTO>>(lista);
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
        public async Task<ActionResult<EstructuraTipoDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetEstructuraTipoById(id);
                if (res == null) return NotFound();

                EstructuraTipoDTO result = mapper.Map<EstructuraTipoDTO>(res);
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
        public async Task<ActionResult<int>> Post(EstructuraTipoDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                EstructuraTipo EstructuraTipo = mapper.Map<EstructuraTipo>(dto);

                var resp = await repositorio.Insert(EstructuraTipo);
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
        public async Task<ActionResult<bool>> Put(EstructuraTipoDTO dto, int id)
        {
            try
            {
                dto.ContratoEstructuras = null;
                EstructuraTipo EstructuraTipo = mapper.Map<EstructuraTipo>(dto);
                var resp = await repositorio.Update(EstructuraTipo, id);
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
                    return Ok($"El EstructuraTipo ha sido dado de baja del sistema");
                }
                return NotFound("El EstructuraTipo a dar de baja no a sido encontrado");
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
        //            return Ok($"El EstructuraTipo ha sido borrado");
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
