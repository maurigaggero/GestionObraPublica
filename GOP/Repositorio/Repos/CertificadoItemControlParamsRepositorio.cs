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
    public class CertificadoItemControlParamsRepositorio : Repositorio<CertificadoItemControlParam>, ICertificadoItemControlParamsRepositorio
    {
        public CertificadoItemControlParamsRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<CertificadoItemControlParam> GetCertificadoItemControlParamById(int id)
        {
            try
            {
                var res = await Context.Set<CertificadoItemControlParam>()
                                    .AsNoTracking()
                                    .Include(e => e.CertificadoItemControl)
                                    .Include(e => e.ContratoItemControlParam)
                                    .Include(e => e.Unidad)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<CertificadoItemControlParam>> GetCertificadoItemControlParams()
        {
            try
            {
                var list = await Context.Set<CertificadoItemControlParam>()
                                    .Include(e => e.CertificadoItemControl)
                                    .Include(e => e.ContratoItemControlParam)
                                    .Include(e => e.Unidad)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
