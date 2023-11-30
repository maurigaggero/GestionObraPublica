using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface ICertificadoRepositorio : IRepositorio<Certificado>
    {
        Task<List<Certificado>> GetCertificados();
        Task<List<Certificado>> GetReporteCertificados();
        Task<List<Certificado>> GetReporteControlCertificados();
        Task<Certificado> GetCertificadoById(int id);
        Task<Certificado> GetCertificadoParaReporteById(int id);
        Task<List<Certificado>> GetCertificadosPorContrato(int idContrato);
        Task<Certificado> GetCertificadoByIdNoFull(int id);
        Task<Certificado> GetCertificadoPorPeriodo(int contratoId, string periodo);
        Task<List<Certificado>> GetReporteCertificadosByContrato(int idContrato);
    }
}
