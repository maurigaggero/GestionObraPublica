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
    public class ItemDocRepositorio : Repositorio<ItemDoc>, IItemDocRepositorio
    {
        private readonly BDContext context;

        public ItemDocRepositorio(BDContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<ItemDoc> GetItemDocById(int id)
        {
            try
            {
                var res = await context.Set<ItemDoc>()
                                    .AsNoTracking()
                                    .Include(e => e.Item)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<ItemDoc>> GetItemDocs()
        {
            try
            {
                var list = await context.Set<ItemDoc>()
                                    .Include(e => e.Item)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
