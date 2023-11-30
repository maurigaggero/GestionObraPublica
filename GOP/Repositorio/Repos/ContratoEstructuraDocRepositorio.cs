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
    public class ContratoEstructuraDocRepositorio : Repositorio<ContratoEstructuraDoc>, IContratoEstructuraDocRepositorio
    {
        public ContratoEstructuraDocRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<ContratoEstructuraDoc> GetContratoEstructuraDocById(int id)
        {
            try
            {
                var res = await Context.Set<ContratoEstructuraDoc>()
                                    .AsNoTracking()
                                    .Include(e => e.ContratoEstructura)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<ContratoEstructuraDoc>> GetContratoEstructuraDocs(int id)
        {
            try
            {
                var list = await Context.Set<ContratoEstructuraDoc>()
                                    .Where(i => i.EstadoRegistro == 0 && i.ContratoEstructuraId == id).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
