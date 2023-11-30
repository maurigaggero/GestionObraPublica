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
    public class FrenteObraProfesionalesRepositorio : Repositorio<FrenteObraProfesional> , IFrenteObraProfesionalesRepositorio
    {
        public FrenteObraProfesionalesRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<FrenteObraProfesional> GetFrenteObraProfesionalfById(int id)
        {
            try
            {
                var res = await Context.Set<FrenteObraProfesional>()
                                    .AsNoTracking()
                                    .Include(e => e.FrenteObra)
                                    .Include(e => e.Persona)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<FrenteObraProfesional>> GetFrenteObraProfesionales()
        {
            try
            {
                var list = await Context.Set<FrenteObraProfesional>()
                                    .Include(e => e.FrenteObra)
                                    .Include(e => e.Persona)
                                    .Where(e => e.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
