using IISPI.BD.Data.Entity;
using IISPI.Repositorio.Repos;
using IISPI.Shared.DTOs.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IISPI.Server.Controllers.IISPIControllers
{
    [ApiController]
    [Route("iispi/api/personas")]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonasRepositorio repositorio;

        public PersonasController(IPersonasRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet("getfull")]
        public async Task<ActionResult<List<Persona>>> GetFull()
        {
            try
            {
                List<Persona> lista = await repositorio.GetFull();
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getactivos")]
        public async Task<ActionResult<List<Persona>>> GetActivos()
        {
            try
            {
                List<Persona> lista = await repositorio.GetActivos();
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Persona>> Get(int id)
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
        public async Task<ActionResult<int>> Post(RegistrarUserDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Datos incorrectos");
                Persona persona = MapeaPersona(dto);

                var resp = await repositorio.Insert(persona);
                if (resp > 0) return Ok(resp);
                else return BadRequest("No se pudo agregar el tipo de estado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> Put(RegistrarUserDTO dto, int id)
        {
            try
            {
                Persona persona = MapeaPersona(dto);
                var resp = await repositorio.Update(persona, id);
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
                    return Ok($"La persona ha sido dada de baja del sistema");
                }
                return NotFound("La persona a dar de baja no a sido encontrada");
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
                    return Ok($"La persona ha sido borrada");
                }
                return NotFound("El Tipo de Estado que desea borrar no a sido encontrado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region METODOS
        public static Persona MapeaPersona(RegistrarUserDTO dto)
        {
            Persona persona = new Persona();
            persona.DNI = dto.DNI;
            persona.Nombre = dto.Nombre;
            persona.Apellido = dto.Apellido;
            return persona;
        }
        #endregion
    }
}
