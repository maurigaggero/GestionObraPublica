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
    public class CertificadoItemControlsRepositorio : Repositorio<CertificadoItemControl>, ICertificadoItemControlsRepositorio
    {
        public CertificadoItemControlsRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<CertificadoItemControl> GetCertificadoItemControlById(int id)
        {
            try
            {
                var res = await Context.Set<CertificadoItemControl>()
                                    .AsNoTracking()
                                    .Include(e => e.CertificadoItem)
                                    .Include(e => e.ContratoItemControl)
                                    .Include(e => e.Documentos)
                                    .Include(e => e.Parametros)
                                    .ThenInclude(x => x.Unidad)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception e) { throw; }
        }

        public async Task<List<CertificadoItemControl>> GetCertificadoItemControls()
        {
            try
            {
                var list = await Context.Set<CertificadoItemControl>()
                                    .Include(e => e.CertificadoItem)
                                    .Include(e => e.ContratoItemControl)
                                    .Include(e => e.Documentos)
                                    .Include(e => e.Parametros)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
