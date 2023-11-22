using AutoMapper;
using IISPI.BD.Data;
using IISPI.BD.Data.Entity;
using IISPI.Repositorio.Repos;
using IISPI.Shared.DTOs.IINSPIDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IISPI.Server.Controllers.IISPIControllers
{
    [ApiController]
    [Route("iispi/api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepositorio repositorio;
        //private readonly IMapper mapper;

        public ItemsController(IItemsRepositorio repositorio)//, IMapper mapper)
        {
            this.repositorio = repositorio;
            //this.mapper = mapper;
        }
        [HttpGet("getfull")]
        public async Task<ActionResult<List<Item>>> GetFull()
        {
            try
            {
                List<Item> lista = await repositorio.GetFull();
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getactivos")]
        public async Task<ActionResult<List<Item>>> GetActivos()
        {
            try
            {
                List<Item> lista = await repositorio.GetActivos();
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            try
            {
                var res = await repositorio.GetById(id);
                if (res == null) return NotFound();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(ItemsDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");

                Item item = Mapeo(dto);

                var resp = await repositorio.Insert(item);
                if (resp > 0) return Ok(resp);
                else return BadRequest("No se pudo agregar el tipo de estado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Put(ItemsDTO dto, int id)
        {
            try
            {
                Item item = Mapeo(dto);
                var resp = await repositorio.Update(item, id);
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
        public async Task<ActionResult> Baja(int id)
        {
            try
            {
                if (await repositorio.Baja(id))
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await repositorio.Delete(id))
                {
                    return Ok($"El Item ha sido borrada");
                }
                return NotFound("El Tipo de Estado que desea borrar no a sido encontrado");
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static Item Mapeo(ItemsDTO dto)
        {
            Item item = new();
            item.CodItem = dto.CodItem;
            item.DescItem = dto.DescItem;
            item.UnidadId = dto.UnidadId;
            return item;
        }
    }
}
