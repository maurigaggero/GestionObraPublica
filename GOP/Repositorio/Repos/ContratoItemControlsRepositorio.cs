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
    public class ContratoItemControlsRepositorio : Repositorio<ContratoItemControl>, IContratoItemControlsRepositorio
    {
        public ContratoItemControlsRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<ContratoItemControl> GetContratoItemControlById(int id)
        {
            try
            {
                var res = await Context.Set<ContratoItemControl>()
                                    .AsNoTracking()
                                    .Include(e => e.Documentos)
                                    .Include(e => e.Parametros)
                                    .ThenInclude(e => e.Unidad)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<ContratoItemControl>> GetContratoItemControls()
        {
            try
            {
                var list = await Context.Set<ContratoItemControl>()
                                    .Include(e => e.Documentos)
                                    .Include(e => e.Parametros)
                                    .ThenInclude(e => e.Unidad)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
