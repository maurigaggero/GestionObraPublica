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
    public class ContratoItemControlDocsRepositorio : Repositorio<ContratoItemControlDoc>, IContratoItemControlDocsRepositorio
    {
        public ContratoItemControlDocsRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<ContratoItemControlDoc> GetContratoItemControlDocById(int id)
        {
            try
            {
                var res = await Context.Set<ContratoItemControlDoc>()
                                    .AsNoTracking()
                                    .Include(e => e.ContratoItemControl)
                                    .Include(e => e.ItemControlDoc)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<ContratoItemControlDoc>> GetContratoItemControlDocs()
        {
            try
            {
                var list = await Context.Set<ContratoItemControlDoc>()
                                    .Include(e => e.ContratoItemControl)
                                    .Include(e => e.ItemControlDoc)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
