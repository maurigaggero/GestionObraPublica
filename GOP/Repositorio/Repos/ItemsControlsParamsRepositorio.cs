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
    public class ItemsControlsParamsRepositorio : Repositorio<ItemControlParam> , IItemsControlsParamsRepositorio
    {
        private readonly BDContext context;
        public ItemsControlsParamsRepositorio(BDContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<ItemControlParam>> GetItemControlParams()
        {
            try
            {
                var list = await context.Set<ItemControlParam>()
                    .Include(i => i.Unidad)
                    .Include(i => i.ItemControl)
                    .ThenInclude(ip => ip.Item)
                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
        public async Task<ItemControlParam> GetItemControlParamById(int id)
        {
            try
            {
                var res = await context.Set<ItemControlParam>()
                                              .AsNoTracking()
                                              .Include(i => i.Unidad)
                                              .Include(i => i.ItemControl)
                                              .ThenInclude(ip => ip.Item)
                                              .FirstOrDefaultAsync(i => i.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }
    }
}
