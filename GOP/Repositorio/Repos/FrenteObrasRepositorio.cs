using GOP.BD.Data;
using GOP.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public class FrenteObrasRepositorio : Repositorio<FrenteObra> , IFrenteObrasRepositorio
    {
        public FrenteObrasRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<FrenteObra> GetFrenteObraByIdNoFull(int id)
        {
            try
            {
                var res = await Context.Set<FrenteObra>()
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<FrenteObra> GetFrenteObraById(int id)
        {
            try
            {
                var res = await Context.Set<FrenteObra>()
                                    .AsNoTracking()
                                    .Include(e => e.Profesionales)
                                    .ThenInclude(e => e.Persona)
                                    .Include(e => e.Zona)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<FrenteObra>> GetFrenteObras()
        {
            try
            {
                var list = await Context.Set<FrenteObra>()
                                    .Include(e => e.Profesionales)
                                    .Include(e => e.Zona)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<FrenteObra>> GetFrenteObrasPorPersona(int idPersona)
        {
            try
            {
                var list = await Context.Set<FrenteObra>()
                    .Where(x => x.Profesionales.Any(x => x.PersonaId == idPersona))
                    .Where(x => x.EstadoRegistro == 0)
                    .ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<FrenteObra>> GetFrenteObrasPorZona(int idZona)
        {
            try
            {
                var res = await Context.Set<FrenteObra>()
                                    .AsNoTracking()
                                    .Include(e => e.Profesionales)
                                    .ThenInclude(e => e.Persona)
                                    .Include(e => e.Zona)
                                    .Where(e => e.ZonaId == idZona)
                                    .Where(e => e.EstadoRegistro == 0)
                                    .ToListAsync();
                return res;
            }
            catch (Exception) { throw; }
        }
    }
}
