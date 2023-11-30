using GOP.BD.Data;
using GOP.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace GOP.Repositorio.Repos
{
    public class CertificadoDocRepositorio : Repositorio<CertificadoDoc>, ICertificadoDocRepositorio
    {
        public CertificadoDocRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<CertificadoDoc> GetCertificadoDocById(int id)
        {
            try
            {
                var res = await Context.Set<CertificadoDoc>()
                                    .AsNoTracking()
                                    .Include(e => e.Certificado)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }


        public async Task<List<CertificadoDoc>> GetCertificadosDocById(int id)
        {
            try
            {
                var list = await Context.Set<CertificadoDoc>()
                                       .Include(e => e.Certificado)
                                       .Where(i => i.EstadoRegistro == 0 && i.CertificadoId == id).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<CertificadoDoc>> GetCertificadoDocs()
        {
            try
            {
                var list = await Context.Set<CertificadoDoc>()
                                    .Include(e => e.Certificado)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
