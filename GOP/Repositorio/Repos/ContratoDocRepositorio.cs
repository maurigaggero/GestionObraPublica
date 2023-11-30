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
    public class ContratoDocRepositorio : Repositorio<ContratoDoc>, IContratoDocRepositorio
    {
        public ContratoDocRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<ContratoDoc> GetContratoDocById(int id)
        {
            try
            {
                var res = await Context.Set<ContratoDoc>()
                                    .AsNoTracking()
                                    .Include(e => e.Contrato)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<ContratoDoc>> GetContratoDocs()
        {
            try
            {
                var list = await Context.Set<ContratoDoc>()
                                    .Include(e => e.Contrato)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<ContratoDoc>> GetContratoDocsByContratoId(int idContrato)
        {
            try
            {
                var list = await Context.Set<ContratoDoc>()
                                    .Where(i => i.ContratoId == idContrato
                                        && i.EstadoRegistro == 0)
                                    .ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
