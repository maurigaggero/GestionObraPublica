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
    public class ItemsRepositorio : Repositorio<Item>, IItemsRepositorio
    {
        private readonly BDContext context;
        public ItemsRepositorio(BDContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Item>> GetItems()
        {
            try
            {
                var list = await context.Set<Item>()
                    .Include(i => i.Unidad)
                    .Include(i => i.ItemControles)
                    .Where(i => i.EstadoRegistro == 0)
                    .OrderByDescending(i => i.CodItem)
                    .ToListAsync();

                return list;
            }
            catch (Exception) { throw; }
        }
        public async Task<Item> GetItemById(int id)
        {
            try
            {
                var res = await context.Set<Item>()
                                              .AsNoTracking()
                                              .Include(i => i.Unidad)
                                              .Include(i => i.ItemControles)
                                              .OrderByDescending(i => i.CodItem)
                                              .FirstOrDefaultAsync(i => i.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }
    }
}
