
using AutoMapper;
using GOP.BD.Data;
using GOP.BD.Data.Entity;
using GOP.Repositorio.Repos;
using GOP.Shared.DTOs;
using GOP.Shared.DTOs.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GOP.Server.Controllers.GOPControllers
{
    [ApiController]
    [Route("GOP/api/ContratoEstructura")]
    public class ContratoEstructuraController : GOPBaseController
    {
        private readonly IContratoEstructuraRepositorio repositorio;
        private readonly IMapper mapper;

        public ContratoEstructuraController(IContratoEstructuraRepositorio repositorio, 
                                            IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoEstructuraDTO>>> GetFull()
        {
            try
            {
                List<ContratoEstructura> lista = await repositorio.GetActivos();

                List<ContratoEstructuraDTO> result = mapper.Map<List<ContratoEstructuraDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getContratoEstructura/{contratoId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoEstructuraDTO>>> GetContratoEstructura(int contratoId) //CON INCLUDE
        {
            try
            {
                List<ContratoEstructura> lista = await repositorio.GetContratoEstructuras(contratoId);

                List<ContratoEstructuraDTO> result = mapper.Map<List<ContratoEstructuraDTO>>(lista);
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
        public async Task<ActionResult<ContratoEstructuraDTO>> Get(int id) //CON INCLUDE
        {
            try
            {

                var res = await repositorio.GetContratoEstructuraById(id);
                if (res == null) return NotFound();

                ContratoEstructuraDTO result = mapper.Map<ContratoEstructuraDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getByContratoId/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoEstructuraDTO>>> GetEstructuraByContrato([FromQuery] PaginacionDTO paginacion, int id) //CON INCLUDE
        {
            try
            {
                List<ContratoEstructura> lista = await repositorio.GetContratoEstructurasByContratoId(paginacion, id);

                HttpContext.Response.Headers.Add("Registros", lista.Count.ToString());

                List<ContratoEstructuraDTO> result = mapper.Map<List<ContratoEstructuraDTO>>(lista);
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
        public async Task<ActionResult<int>> Post(ContratoEstructuraDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                ContratoEstructura entidad = mapper.Map<ContratoEstructura>(dto);

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<ContratoEstructura>(entidad);
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
        public async Task<ActionResult<bool>> Put(ContratoEstructuraDTO dto, int id)
        {
            try
            {
                dto.EstructuraTipo = null;
                dto.Calle = null;
                dto.CertificadoItems = null;
                dto.Contrato = null;
                ContratoEstructura entidad = mapper.Map<ContratoEstructura>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<ContratoEstructura>(entidad);
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
                    return Ok($"El ContratoEstructura ha sido dado de baja del sistema");
                }
                return NotFound("El ContratoEstructura a dar de baja no a sido encontrado");
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
        //            return Ok($"El ContratoEstructura ha sido borrado");
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
