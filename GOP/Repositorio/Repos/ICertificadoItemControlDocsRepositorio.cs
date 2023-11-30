using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface ICertificadoItemControlDocsRepositorio : IRepositorio<CertificadoItemControlDoc>
    {
        Task<List<CertificadoItemControlDoc>> GetCertificadoItemControlDocs(int id);
        Task<CertificadoItemControlDoc> GetCertificadoItemControlDocById(int id);
    }
}
