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
    public class ContratoItemControlParamsRepositorio : Repositorio<ContratoItemControlParam>, IContratoItemControlParamsRepositorio
    {
        public ContratoItemControlParamsRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<ContratoItemControlParam> GetContratoItemControlParamById(int id)
        {
            try
            {
                var res = await Context.Set<ContratoItemControlParam>()
                                    .AsNoTracking()
                                    .Include(e => e.Unidad)
                                    .Include(e => e.ContratoItemControl)
                                    .Include(e => e.ItemControlParam)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<ContratoItemControlParam>> GetContratoItemControlParams()
        {
            try
            {
                var list = await Context.Set<ContratoItemControlParam>()
                                    .Include(e => e.Unidad)
                                    .Include(e => e.ContratoItemControl)
                                    .Include(e => e.ItemControlParam)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
