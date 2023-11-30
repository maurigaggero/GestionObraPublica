using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface ICertificadoItemDefRepositorio : IRepositorio<CertificadoItemDef>
    {
        Task<List<CertificadoItemDef>> GetCertificadosDefByIdCertificado(int id);
        bool DeleteUpdate(List<CertificadoItemDef> certificadoItemDefs, List<CertificadoItem> certitems);
        Task<List<CertificadoItemDef>> GetCertificadosDefByIdContrato(int idContrato);
    }
}
