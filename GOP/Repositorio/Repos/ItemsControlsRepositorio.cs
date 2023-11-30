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
    public class ItemsControlsRepositorio : Repositorio<ItemControl>, IItemsControlsRepositorio
    {
        private readonly BDContext context;
        public ItemsControlsRepositorio(BDContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<ItemControl>> GetItemControls()
        {
            try
            {
                var list = await context.Set<ItemControl>()
                    .Include(i => i.Item)
                    .ThenInclude(iu => iu.Unidad)
                    .Include(i => i.Documentos)
                    .Include(i => i.Parametros)
                    .ThenInclude(ip => ip.Unidad)
                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
        public async Task<ItemControl> GetItemControlById(int id)
        {
            try
            {
                var res = await context.Set<ItemControl>()
                                              .AsNoTracking()
                                              .Include(i => i.Item)
                                              .ThenInclude(iu => iu.Unidad)
                                              .Include(i => i.Documentos)
                                              .Include(i => i.Parametros)
                                              .ThenInclude(ip => ip.Unidad)
                                              .FirstOrDefaultAsync(i => i.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }
        public async Task<List<ItemControl>> GetItemControlsByItem(int idItem)
        {
            try
            {
                var list = await context.Set<ItemControl>()
                    .AsNoTracking()
                    .Include(i => i.Item)
                    .Include(i => i.Documentos)
                    .Include(i => i.Parametros)
                    .ThenInclude(ip => ip.Unidad)
                    .Where(i => i.ItemId == idItem)
                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
