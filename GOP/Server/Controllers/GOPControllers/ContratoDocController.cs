using AutoMapper;
using GOP.BD.Data;
using GOP.BD.Data.Entity;
using GOP.Repositorio.Repos;
using GOP.Server.Helpers;
using GOP.Shared.DTOs.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GOP.Server.Controllers.GOPControllers
{
    [ApiController]
    [Route("GOP/api/ContratoDoc")]
    public class ContratoDocController : GOPBaseController
    {
        private readonly IContratoDocRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;

        public ContratoDocController(IContratoDocRepositorio repositorio, 
                                     IMapper mapper, 
                                     IAlmacenadorArchivos almacenadorArchivos)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoDocDTO>>> GetFull()
        {
            try
            {
                List<ContratoDoc> lista = await repositorio.GetActivos();

                List<ContratoDocDTO> result = mapper.Map<List<ContratoDocDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getContratoDoc")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoDocDTO>>> GetContratoDoc() //CON INCLUDE
        {
            try
            {
                List<ContratoDoc> lista = await repositorio.GetContratoDocs();

                List<ContratoDocDTO> result = mapper.Map<List<ContratoDocDTO>>(lista);
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
        public async Task<ActionResult<ContratoDocDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetContratoDocById(id);
                if (res == null) return NotFound();

                ContratoDocDTO result = mapper.Map<ContratoDocDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getByContratoId/{idContrato:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoDocDTO>>> GetContratoDocByContratoId(int idContrato)
        {
            try
            {
                List<ContratoDoc> lista = await repositorio.GetContratoDocsByContratoId(idContrato);

                List<ContratoDocDTO> result = mapper.Map<List<ContratoDocDTO>>(lista);
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
        public async Task<ActionResult<int>> Post(ContratoDocDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                ContratoDoc entidad = mapper.Map<ContratoDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.GuardarArchivo(archivo, dto.Extension, "Contratos");
                }

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<ContratoDoc>(entidad);
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
        public async Task<ActionResult<bool>> Put(ContratoDocDTO dto, int id)
        {
            try
            {
                dto.Contrato = null;
                ContratoDoc entidad = mapper.Map<ContratoDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.GuardarArchivo(archivo, dto.Extension, "Contratos");
                }

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<ContratoDoc>(entidad);
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
                    return Ok($"El ContratoDoc ha sido dado de baja del sistema");
                }
                return NotFound("El ContratoDoc a dar de baja no a sido encontrado");
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
        //        ContratoDocDTO doc = Get(id).Result.Value;
        //        await almacenadorArchivos.EliminarArchivo(doc.PathDoc);

        //        if (await repositorio.Delete(id))
        //        {
        //            return Ok($"El ContratoDoc ha sido borrado");
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
