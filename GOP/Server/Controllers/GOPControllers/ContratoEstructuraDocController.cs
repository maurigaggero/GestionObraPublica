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
    [Route("GOP/api/ContratoEstructuraDoc")]
    public class ContratoEstructuraDocController : GOPBaseController
    {
        private readonly IContratoEstructuraDocRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;

        public ContratoEstructuraDocController(IContratoEstructuraDocRepositorio repositorio, 
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
        public async Task<ActionResult<List<ContratoEstructuraDocDTO>>> GetFull()
        {
            try
            {
                List<ContratoEstructuraDoc> lista = await repositorio.GetActivos();

                List<ContratoEstructuraDocDTO> result = mapper.Map<List<ContratoEstructuraDocDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getContratoEstructuraDoc/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ContratoEstructuraDocDTO>>> GetContratoEstructuraDoc(int id) //CON INCLUDE
        {
            try
            {
                List<ContratoEstructuraDoc> lista = await repositorio.GetContratoEstructuraDocs(id);

                List<ContratoEstructuraDocDTO> result = mapper.Map<List<ContratoEstructuraDocDTO>>(lista);
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
        public async Task<ActionResult<ContratoEstructuraDocDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetContratoEstructuraDocById(id);
                if (res == null) return NotFound();

                ContratoEstructuraDocDTO result = mapper.Map<ContratoEstructuraDocDTO>(res);
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
        public async Task<ActionResult<int>> Post(ContratoEstructuraDocDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                ContratoEstructuraDoc entidad = mapper.Map<ContratoEstructuraDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.GuardarArchivo(archivo, dto.Extension, "ContratoEstructuras");
                }

                #region Actualiza idcrea entidad
                ActualizaEntidadBase<ContratoEstructuraDoc>(entidad);
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
        public async Task<ActionResult<bool>> Put(ContratoEstructuraDocDTO dto, int id)
        {
            try
            {
                dto.ContratoEstructura = null;
                ContratoEstructuraDoc entidad = mapper.Map<ContratoEstructuraDoc>(dto);

                if (!String.IsNullOrEmpty(dto.File) && !String.IsNullOrEmpty(dto.Extension))
                {
                    var archivo = Convert.FromBase64String(dto.File);
                    entidad.PathDoc = await almacenadorArchivos.GuardarArchivo(archivo, dto.Extension, "ContratoEstructuras");
                }

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<ContratoEstructuraDoc>(entidad);
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
                //ContratoEstructuraDocDTO doc = Get(id).Result.Value;
                //await almacenadorArchivos.EliminarArchivo(doc.PathDoc);
                if (await repositorio.Baja(id, ObtenerUserId()))
                {
                    return Ok($"El ContratoEstructuraDoc ha sido dado de baja del sistema");
                }
                return NotFound("El ContratoEstructuraDoc a dar de baja no a sido encontrado");
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
        //        ContratoEstructuraDocDTO doc = Get(id).Result.Value;
        //        await almacenadorArchivos.EliminarArchivo(doc.PathDoc);
        //        if (await repositorio.Delete(id))
        //        {
        //            return Ok($"El ContratoEstructuraDoc ha sido borrado");
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
