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
    public class EstructuraTipoRepositorio : Repositorio<EstructuraTipo>, IEstructuraTipoRepositorio
    {
        public EstructuraTipoRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<EstructuraTipo> GetEstructuraTipoById(int id)
        {
            try
            {
                var res = await Context.Set<EstructuraTipo>()
                                    .AsNoTracking()
                                    .Include(e => e.ContratoEstructuras)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<EstructuraTipo>> GetEstructuraTipos()
        {
            try
            {
                var list = await Context.Set<EstructuraTipo>()
                                    .Include(e => e.ContratoEstructuras)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
