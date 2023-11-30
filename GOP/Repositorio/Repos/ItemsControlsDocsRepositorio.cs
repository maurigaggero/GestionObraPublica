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
    public class ItemsControlsDocsRepositorio : Repositorio<ItemControlDoc> , IItemsControlsDocsRepositorio
    {
        private readonly BDContext context;
        public ItemsControlsDocsRepositorio(BDContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<ItemControlDoc>> GetItemControls()
        {
            try
            {
                var list = await context.Set<ItemControlDoc>()
                    .Include(i => i.ItemControl)
                    .ThenInclude(ic => ic.Item)
                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
        public async Task<ItemControlDoc> GetItemControlById(int id)
        {
            try
            {
                var res = await context.Set<ItemControlDoc>()
                                              .AsNoTracking()
                                              .Include(i => i.ItemControl)
                                              .ThenInclude(ic => ic.Item)
                                              .FirstOrDefaultAsync(i => i.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }
    }
}
