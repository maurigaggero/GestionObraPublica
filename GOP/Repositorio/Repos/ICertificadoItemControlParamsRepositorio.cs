using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface ICertificadoItemControlParamsRepositorio : IRepositorio<CertificadoItemControlParam>
    {
        Task<List<CertificadoItemControlParam>> GetCertificadoItemControlParams();
        Task<CertificadoItemControlParam> GetCertificadoItemControlParamById(int id);
    }
}
