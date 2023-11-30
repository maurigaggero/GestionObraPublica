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
    public class CertificadoItemRepositorio : Repositorio<CertificadoItem>, ICertificadoItemsRepositorio
    {
        public CertificadoItemRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<CertificadoItem> GetCertificadoItemById(int id)
        {
            try
            {
                var res = await Context.Set<CertificadoItem>()
                                    .AsNoTracking()
                                    .Include(e => e.Certificado)
                                    .ThenInclude(e => e.Contrato)
                                    .Include(e => e.Unidad)
                                    .Include(e => e.ItemContrato)
                                    .Include(e => e.FrenteObra)
                                    .Include(e => e.ContratoEstructura)
                                    .Include(e => e.CertificadoItemControls)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<CertificadoItem>> GetCertificadoItems(int id)
        {
            try
            {
                var list = await Context.Set<CertificadoItem>()
                                    .AsNoTracking()
                                    .Include(e => e.Certificado)
                                    .Include(e => e.Unidad)
                                    .Include(e => e.ItemContrato)
                                    .Include(e => e.FrenteObra)
                                    .Include(e => e.CertificadoItemControls)
                                    .Include(e => e.ContratoEstructura)
                                    .OrderBy(x => x.CodItem).ThenBy(x => x.FechaMedicion)
                                    .Where(i => i.EstadoRegistro == 0 && i.CertificadoId == id).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<CertificadoItem>> GetCertificadoItemsSingle(int id)
        {                               //NO POSEE INCLUDES PARA MANDAR LA ENTIDAD CRUDA
            try
            {
                var list = await Context.Set<CertificadoItem>()
                                    .AsNoTracking()
                                    .OrderBy(x => x.CodItem).ThenBy(x => x.FechaMedicion)
                                    .Where(i => i.EstadoRegistro == 0 && i.CertificadoId == id).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<CertificadoItem>> GetCertificadoItemsByIdEstructura(int idEstructura)
        {
            try
            {
                var list = await Context.Set<CertificadoItem>()
                                    .AsNoTracking()
                                    .Include(x => x.Unidad)
                                    .Where(i => i.EstadoRegistro == 0 && i.ContratoEstructuraId == idEstructura)
                                    .OrderBy(x => x.CodItem).ThenBy(x => x.FechaMedicion)
                                    .ToListAsync();

                                    //.Where(i => i.EstadoRegistro == 0 && i.ContratoEstructura.EstructuraTipoId == idEstructura)

                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<CertificadoItem>> GetCertificadoItemsByIdFrente(int idFrente)
        {
            try
            {
                //var list = await Context.Set<CertificadoItem>()                               //MAL SERGIO
                //                    .AsNoTracking()
                //                    .OrderBy(x => x.CodItem).ThenBy(x => x.FechaMedicion)
                //                    .Where(i => i.EstadoRegistro == 0 && i.FrenteObraId == idFrente)
                //                    .ToListAsync();
                var list = await Context.Set<CertificadoItem>().AsNoTracking()
                                    .Include(x => x.Unidad)
                                    .Include(x => x.Certificado)
                                    .OrderBy(x => x.CodItem)
                                    .ThenBy(x => x.Certificado.Periodo.Substring(2,4))
                                    .ThenBy(x => x.Certificado.Periodo.Substring(0,2))
                                    .Where(i => i.EstadoRegistro == 0 && i.FrenteObraId == idFrente)
                                    .ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<ContratoItem> GetItemDelCertificado(int id)
        {
            try
            {
                var res = await Context.Set<ContratoItem>()
                                    .AsNoTracking()
                                    .Include(e => e.ContratoItemControls)
                                    .ThenInclude(i => i.Parametros)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }
    }
}
