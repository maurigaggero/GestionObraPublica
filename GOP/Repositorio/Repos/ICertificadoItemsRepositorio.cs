using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface ICertificadoItemsRepositorio : IRepositorio<CertificadoItem>
    {
        Task<List<CertificadoItem>> GetCertificadoItems(int id);
        Task<List<CertificadoItem>> GetCertificadoItemsSingle(int id);
        Task<List<CertificadoItem>> GetCertificadoItemsByIdEstructura(int idEstructura);
        Task<List<CertificadoItem>> GetCertificadoItemsByIdFrente(int idFrente);
        Task<CertificadoItem> GetCertificadoItemById(int id);
        Task<ContratoItem> GetItemDelCertificado(int id);
    }
}
