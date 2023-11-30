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
    public class ContratoItemRepositorio : Repositorio<ContratoItem>, IContratoItemsRepositorio
    {
        public ContratoItemRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<ContratoItem> GetContratoItemById(int id)
        {
            try
            {
                var res = await Context.Set<ContratoItem>()
                                    .AsNoTracking()
                                    .Include(e => e.Contrato)
                                    .Include(e => e.Item)
                                    .Include(e => e.ContratoItemControls)
                                    .ThenInclude(e => e.Parametros)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<ContratoItem>> GetContratoItems()
        {
            try
            {
                var list = await Context.Set<ContratoItem>()
                                    .Include(e => e.Contrato)
                                    .Include(e => e.Item)
                                    .Include(e => e.ContratoItemControls)
                                    .ThenInclude(e => e.Parametros)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<Item> GetItemDelContrato(int id)
        {
            try
            {
                var res = await Context.Set<Item>()
                                    .AsNoTracking()
                                    .Include(e => e.ItemControles)
                                    .ThenInclude(i => i.Parametros)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<ContratoItem>> GetContratoItemsByContratoId(int idContrato)
        {
            try
            {
                var list = await Context.Set<ContratoItem>()
                                    .Where(i => i.ContratoId == idContrato
                                        && i.EstadoRegistro == 0)
                                    .Include(e => e.Contrato)
                                    .Include(e => e.Item)
                                    .ThenInclude(i => i.Unidad)
                                    .ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
