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
    public class ZonasRepositorio : Repositorio<Zona>, IZonasRepositorio
    {
        private readonly BDContext context;

        public ZonasRepositorio(BDContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Zona> GetZonaByIdNoFull(int id)
        {
            try
            {
                var res = await context.Set<Zona>()
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<Zona> GetZonaById(int id)
        {
            try
            {
                var res = await context.Set<Zona>()
                                    .AsNoTracking()
                                    .Include(e => e.Profesionales)
                                    .ThenInclude(e => e.Persona)
                                    .Include(e => e.Eventos)
                                    .Include(e => e.Contratos)
                                    .Include(e => e.FrenteObras)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Zona>> GetZonas()
        {
            try
            {
                var list = await context.Set<Zona>()
                                    .Include(e => e.Profesionales)
                                    .Include(e => e.Eventos)
                                    .Include(e => e.Contratos)
                                    .Include(e => e.FrenteObras)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
        public async Task<List<Zona>> GetZonasPorPersona(int idPersona)
        {
            try
            {
                var list = await context.Set<Zona>()
                    .Where(x => x.Profesionales.Any(x => x.PersonaId == idPersona))
                    .ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
