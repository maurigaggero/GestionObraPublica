using GOP.BD.Data;
using GOP.BD.Data.Entity;
using GOP.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public class ContratoEstructuraRepositorio : Repositorio<ContratoEstructura>, IContratoEstructuraRepositorio
    {
        public ContratoEstructuraRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<ContratoEstructura> GetContratoEstructuraById(int id)
        {
            try
            {
                var res = await Context.Set<ContratoEstructura>()
                                    .AsNoTracking()
                                    .Include(e => e.EstructuraTipo)
                                    .Include(e => e.CertificadoItems)
                                    .Include(e => e.Contrato)
                                    .ThenInclude(e => e.Zona)
                                    .Include(e => e.Calle)
                                    .Include(e => e.EntreCalle)
                                    .Include(e => e.YCalle)
                                    .Include(e => e.EsquinaCalle)
                                    .Include(e => e.Documentos)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<ContratoEstructura>> GetContratoEstructuras(int contratoId)
        {
            try
            {
                var list = await Context.Set<ContratoEstructura>()
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<ContratoEstructura>> GetContratoEstructurasByContratoId(PaginacionDTO paginacion, int id)
        {
            try
            {
                var res = Context.Set<ContratoEstructura>()
                                    .Where(i => i.EstadoRegistro == 0 && i.ContratoId == id)
                                    .AsQueryable();

                var list = await res
                           .OrderByDescending(i => i.Id)
                           .Paginar(paginacion).ToListAsync();

                return list;
            }
            catch (Exception) { throw; }
        }                         
    }
}
