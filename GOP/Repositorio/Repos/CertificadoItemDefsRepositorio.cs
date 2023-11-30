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
    public class CertificadoItemDefsRespositorio : Repositorio<CertificadoItemDef>, ICertificadoItemDefRepositorio
    {
        private readonly BDContext context;

        public CertificadoItemDefsRespositorio(BDContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<CertificadoItemDef>> GetCertificadosDefByIdCertificado(int id)
        {
            try
            {
                var list = await Context.Set<CertificadoItemDef>()
                                       .Where(i => i.EstadoRegistro == 0 && i.CertificadoId == id)
                                       .ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
        public async Task<List<CertificadoItemDef>> GetCertificadosDefByIdContrato(int idContrato)
        {
            try
            {
                var list = await Context.Set<CertificadoItemDef>()
                                       .Include(i => i.ItemContrato)
                                       .Where(i => i.EstadoRegistro == 0 && i.ItemContrato.ContratoId == idContrato)
                                       .ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
        public bool DeleteUpdate(List<CertificadoItemDef> ciDef, List<CertificadoItem> certitems)
        {
            try
            {
                if (ciDef.Count == 0) return false;

                Context.Set<CertificadoItem>().UpdateRange(certitems);
                Context.SaveChanges();
                Context.Set<CertificadoItemDef>().RemoveRange(ciDef);
                Context.SaveChanges();
                return true;
            }
            catch (Exception) { throw; }
        }
    }
}
