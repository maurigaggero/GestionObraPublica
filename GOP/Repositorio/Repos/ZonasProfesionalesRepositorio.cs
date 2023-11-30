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
    public class ZonasProfesionalesRepositorio : Repositorio<ZonaProfesional> , IZonasProfesionalesRepositorio
    {
        private readonly BDContext context;

        public ZonasProfesionalesRepositorio(BDContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<ZonaProfesional> GetZonaProfesionalfById(int id)
        {
            try
            {
                var res = await context.Set<ZonaProfesional>()
                                    .AsNoTracking()
                                    .Include(e => e.Zona)
                                    .Include(e => e.Persona)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<ZonaProfesional>> GetZonasProfesionales(int zonaId)
        {
            try
            {
                var list = await context.Set<ZonaProfesional>()
                                    .Include(e => e.Zona)
                                    .Include(e => e.Persona)
                                    .Where(e => e.EstadoRegistro == 0 && e.ZonaId == zonaId).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
