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
    public class CertificadoRepositorio : Repositorio<Certificado>, ICertificadoRepositorio
    {
        public CertificadoRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<Certificado> GetCertificadoByIdNoFull(int id)
        {
            try
            {
                var res = await Context.Set<Certificado>()
                                    .Include(e => e.Contrato)
                                    .Include(e => e.EmpresaProfesional)
                                    .ThenInclude(i => i.Empresa)
                                    .Include(e => e.ZonaProfesional)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<Certificado> GetCertificadoById(int id)
        {
            try
            {
                var res = await Context.Set<Certificado>()
                                    .AsNoTracking()
                                    .Include(e => e.CertificadoItems)
                                    .Include(e => e.Eventos)
                                    .Include(e => e.Contrato)
                                    .Include(e => e.EmpresaProfesional)
                                    .ThenInclude(i => i.Empresa)
                                    .Include(e => e.ZonaProfesional)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<Certificado> GetCertificadoParaReporteById(int id)
        {
            try
            {
                var res = await Context.Set<Certificado>()
                                    .AsNoTracking()
                                    .Include(e => e.CertificadoItems.OrderBy(i => i.CodItem).ThenBy(i => i.FechaMedicion))
                                    .ThenInclude(i => i.Unidad)
                                    .Include(e => e.CertificadoItems.OrderBy(i => i.CodItem).ThenBy(i => i.FechaMedicion))
                                    .ThenInclude(i => i.FrenteObra)
                                    .Include(e => e.CertificadoItems.OrderBy(i => i.CodItem).ThenBy(i => i.FechaMedicion))
                                    .ThenInclude(i => i.ContratoEstructura)
                                    .Include(e => e.Contrato)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<Certificado> GetCertificadoPorPeriodo(int contratoId, string periodo)
        {
            try
            {
                var res = await Context.Set<Certificado>()
                                    .AsNoTracking()
                                    .Include(e => e.CertificadoItems)
                                    .FirstOrDefaultAsync(e => e.Periodo == periodo && e.ContratoId==contratoId);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Certificado>> GetCertificados()
        {
            try
            {
                var list = await Context.Set<Certificado>()
                                    .Include(e => e.CertificadoItems)
                                    .Include(e => e.Eventos)
                                    .Include(e => e.Contrato)
                                    .Include(e => e.EmpresaProfesional)
                                    .Include(e => e.ZonaProfesional)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Certificado>> GetReporteCertificados()
        {
            try
            {
                var list = await Context.Set<Certificado>()
                                    .Include(e => e.CertificadoItems)
                                    .Include(e => e.Contrato)
                                    .ThenInclude(ep => ep.Empresa)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Certificado>> GetReporteControlCertificados()
        {
            try
            {
                var list = await Context.Set<Certificado>()
                                    .Include(e => e.Contrato)
                                    .ThenInclude(ep => ep.Empresa)
                                    .Include(e => e.CertificadoItems)
                                    .ThenInclude(ic => ic.CertificadoItemControls)
                                    .ThenInclude(ic => ic.Parametros)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Certificado>> GetCertificadosPorContrato(int idContrato)   //sergio
        {
            try
            {
                var list = await Context.Set<Certificado>()
                                    .Where(e => e.ContratoId == idContrato)
                                    .Where(e => e.EstadoRegistro == 0)
                                    .ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Certificado>> GetReporteCertificadosByContrato(int idContrato)   //sergio
        {
            try
            {
                var list = await Context.Set<Certificado>()
                                    .Include(e => e.CertificadoItems)
                                    .Where(e => e.ContratoId == idContrato)
                                    .Where(e => e.EstadoRegistro == 0)
                                    .ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
