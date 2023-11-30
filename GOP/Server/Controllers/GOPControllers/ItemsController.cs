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
    [Route("GOP/api/items")]
    public class ItemsController : GOPBaseController
    {
        private readonly IItemsRepositorio repositorio;
        private readonly IMapper mapper;

        public ItemsController(IItemsRepositorio repositorio, 
                               IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet("getfull")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ItemDTO>>> GetFull()
        {
            try
            {
                List<Item> lista = await repositorio.GetActivos();

                List<ItemDTO> result = mapper.Map<List<ItemDTO>>(lista);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getitems")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos, Zona1, Zona2, Frente, Consulta1, Consulta2")]
        public async Task<ActionResult<List<ItemDTO>>> GetItems() //CON INCLUDE
        {
            try
            {
                List<Item> lista = await repositorio.GetItems();

                List<ItemDTO> result = mapper.Map<List<ItemDTO>>(lista);
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
        public async Task<ActionResult<ItemDTO>> Get(int id) //CON INCLUDE
        {
            try
            {
                var res = await repositorio.GetItemById(id);
                if (res == null) return NotFound();

                ItemDTO result = mapper.Map<ItemDTO>(res);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpGet("getactivos")]
        //public async Task<ActionResult<List<ItemDTO>>> GetActivos()
        //{
        //    try
        //    {
        //        List<Item> lista = await repositorio.GetActivos();

        //        List<ItemDTO> result = mapper.Map<List<ItemDTO>>(lista);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = "Admin, BaseDatos")]
        public async Task<ActionResult<int>> Post(ItemDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                Item entidad = mapper.Map<Item>(dto);

                #region Actualiza idcrear entidad
                ActualizaEntidadBase<Item>(entidad);
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
        public async Task<ActionResult<bool>> Put(ItemDTO dto, int id)
        {
            try
            {
                dto.Unidad = null;
                dto.ItemControles = null;
                Item entidad = mapper.Map<Item>(dto);

                #region Actualiza idmodif entidad
                ActualizaEntidadBase<Item>(entidad);
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
                    return Ok($"El Item ha sido dada de baja del sistema");
                }
                return NotFound("El Item a dar de baja no a sido encontrado");
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
        //            return Ok($"El Item ha sido borrada");
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
