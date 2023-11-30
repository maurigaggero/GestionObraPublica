using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface ICertificadoDocRepositorio : IRepositorio<CertificadoDoc>
    {
        Task<List<CertificadoDoc>> GetCertificadoDocs();
        Task<CertificadoDoc> GetCertificadoDocById(int id);
        Task <List<CertificadoDoc>> GetCertificadosDocById(int id);
    }
}
