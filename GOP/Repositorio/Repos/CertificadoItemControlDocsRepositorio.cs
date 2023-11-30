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
    public class CertificadoItemControlDocsRepositorio : Repositorio<CertificadoItemControlDoc>, ICertificadoItemControlDocsRepositorio
    {
        public CertificadoItemControlDocsRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<CertificadoItemControlDoc> GetCertificadoItemControlDocById(int id)
        {
            try
            {
                var res = await Context.Set<CertificadoItemControlDoc>()
                                    .AsNoTracking()
                                    .Include(e => e.CertificadoItemControl)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<CertificadoItemControlDoc>> GetCertificadoItemControlDocs(int itemControlId)
        {
            try
            {
                var list = await Context.Set<CertificadoItemControlDoc>()
                                    .Include(e => e.CertificadoItemControl)
                                    .Where(i => i.EstadoRegistro == 0 && i.CertificadoItemControlId == itemControlId).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
