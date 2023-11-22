using IISPI.BD.Data.Entity;
using IISPI.Repositorio.Repos;
using IISPI.Shared.DTOs.IINSPIDtos;
using Microsoft.AspNetCore.Mvc;

namespace IISPI.Server.Controllers.IISPIControllers
{
    [ApiController]
    [Route("iispi/api/unidades")]
    public class UnidadesController : ControllerBase
    {
        private readonly IUnidadesRepositorio repositorio;

        public UnidadesController(IUnidadesRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet("getfull")]
        public async Task<ActionResult<List<Unidad>>> GetFull()
        {
            try
            {
                List<Unidad> lista = await repositorio.GetFull();
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getactivos")]
        public async Task<ActionResult<List<Unidad>>> GetActivos()
        {
            try
            {
                List<Unidad> lista = await repositorio.GetActivos();
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Unidad>> Get(int id)
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
        public async Task<ActionResult<int>> Post(UnidadesDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");
                Unidad unidad = MapeaUnidad(dto);

                var resp = await repositorio.Insert(unidad);
                if (resp > 0) return Ok(resp);
                else return BadRequest("No se pudo agregar el tipo de estado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Put(UnidadesDTO dto, int id)
        {
            try
            {
                Unidad unidad = MapeaUnidad(dto);
                var resp = await repositorio.Update(unidad, id);
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
                    return Ok($"La unidad ha sido dada de baja del sistema");
                }
                return NotFound("La unidad a dar de baja no a sido encontrada");
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
                    return Ok($"La unidad ha sido borrada");
                }
                return NotFound("El Tipo de Estado que desea borrar no a sido encontrado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region METODOS
        public static Unidad MapeaUnidad(UnidadesDTO dto)
        {
            Unidad unidad = new Unidad();
            unidad.CodUnidad = dto.CodUnidad;
            unidad.DescUnidad = dto.DescUnidad;
            return unidad;
        }
        #endregion
    }
}
