using AutoMapper;
using IISPI.BD.Data.Entity;
using IISPI.Repositorio.Repos;
using IISPI.Shared.DTOs.IINSPIDtos;
using Microsoft.AspNetCore.Mvc;

namespace IISPI.Server.Controllers.IISPIControllers
{
    [ApiController]
    [Route("iispi/api/itemscontrolsparams")]
    public class ItemsControlsParamsController : Controller
    {
        private readonly IItemsControlsParamsRepositorio repositorio;
        //private readonly IMapper mapper;
        public ItemsControlsParamsController(IItemsControlsParamsRepositorio repositorio)//, IMapper mapper)
        {
            this.repositorio = repositorio;
            //this.mapper = mapper;
        }
        [HttpGet("getfull")]
        public async Task<ActionResult<List<ItemControlParam>>> GetFull()
        {
            try
            {
                List<ItemControlParam> lista = await repositorio.GetFull();
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getactivos")]
        public async Task<ActionResult<List<ItemControlParam>>> GetActivos()
        {
            try
            {
                List<ItemControlParam> lista = await repositorio.GetActivos();
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ItemControlParam>> Get(int id)
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
        public async Task<ActionResult<int>> Post(ItemsControlsParamsDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");
                ItemControlParam icp = Mapeo(dto);

                var resp = await repositorio.Insert(icp);
                if (resp > 0) return Ok(resp);
                else return BadRequest("No se pudo agregar el tipo de estado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Put(ItemsControlsParamsDTO dto, int id)
        {
            try
            {
                ItemControlParam icp = Mapeo(dto);

                var resp = await repositorio.Update(icp, id);
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
                    return Ok($"El Item ha sido borrado");
                }
                return NotFound("El Tipo de Estado que desea borrar no a sido encontrado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static ItemControlParam Mapeo(ItemsControlsParamsDTO dto)
        {
            ItemControlParam itp = new();
            itp.Parametro = dto.Parametro;
            itp.UnidadId = dto.UnidadId;
            itp.ItemControlId = dto.ItemControlId;
            itp.Descripción = dto.Descripción;
            itp.ValorMaximo = dto.ValorMaximo;
            itp.ValorMinimo = dto.ValorMinimo;
            return itp;
        }
    }
}
