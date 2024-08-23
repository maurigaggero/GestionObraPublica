using GOP.BD.Data;
using GOP.BD.Data.Entity;
using GOP.Shared.DTOs;
using GOP.Shared.DTOs.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public class EventoRepositorio : Repositorio<Evento>, IEventoRepositorio
    {
        public EventoRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<Evento> GetEventoById(int id)
        {
            try
            {
                var res = await Context.Set<Evento>()
                                    .AsNoTracking()
                                    .Include(e => e.Empresa)
                                    .Include(e => e.Zona)
                                    .Include(e => e.Tipo)
                                    .Include(e => e.Contrato)
                                    .Include(e => e.Certificado)
                                    .Include(e => e.Documentos.Where(i => i.EstadoRegistro == 0))
                                    .Include(e => e.Prametros)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Evento>> GetEventos(FiltroEventoDTO filtro)
        {
            try
            {
                var res = Context.Set<Evento>()
                                    .Include(e => e.Tipo)
                                    .OrderByDescending(e => e.Fecha).ThenBy(e => e.Tipo.CodTipo)
                                    .Where(i => i.EstadoRegistro == 0
                                    && Convert.ToDateTime(i.Fecha.Date.ToString("dd/MM/yyyy")) >= Convert.ToDateTime(filtro.FechaDesde).Date
                                    && Convert.ToDateTime(i.Fecha.Date.ToString("dd/MM/yyyy")) <= Convert.ToDateTime(filtro.FechaHasta).Date)
                                    .AsQueryable();

                if (filtro.TipoId.HasValue && filtro.TipoId != 0)
                {
                    res = res.Where(i => i.EventoTipoId == filtro.TipoId);
                }
                if (filtro.ContratoId.HasValue && filtro.ContratoId != 0)
                {
                    res = res.Where(i => i.ContratoId == filtro.ContratoId);
                }

                PaginacionDTO paginacion = new()
                {
                    Pagina = filtro.Pagina,
                    RegistrosPorPagina = filtro.RegistrosPorPagina
                };

                var list = await res
                                .OrderByDescending(x => x.Id)
                                .Paginar(paginacion).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<Evento>> GetReporteEventos(FiltroEventoReporteDTO filtro)
        {
            try
            {
                var res = Context.Set<Evento>()
                    .Include(e => e.Tipo)
                    .Where(i => i.EstadoRegistro == 0
                    && i.Fecha.Date >= Convert.ToDateTime(filtro.FechaDesde).Date
                    && i.Fecha.Date <= Convert.ToDateTime(filtro.FechaHasta).Date)
                    .OrderByDescending(e => e.Fecha)
                    .ThenBy(e => e.Contrato.Caratula)
                    .ThenBy(e => e.Certificado.Periodo)
                    .AsQueryable();

                //var res = Context.Set<Evento>()
                //                    .Include(e => e.Tipo)
                //                    .Include(e => e.Contrato)
                //                    .ThenInclude(x => x.Zona)
                //                    .Include(e => e.Certificado)
                //                    .Include(e => e.FrenteObra)
                //                    .Where(i => i.EstadoRegistro == 0
                //                    && i.Fecha.Date >= Convert.ToDateTime(filtro.FechaDesde).Date
                //                    && i.Fecha.Date <= Convert.ToDateTime(filtro.FechaHasta).Date)
                //                    .OrderByDescending(e => e.Fecha)
                //                    .OrderBy(e => e.Zona.CodigoZona)
                //                    .OrderBy(e => e.FrenteObra.CodFrenteObra)
                //                    .OrderBy(e => e.Certificado.Periodo)
                //                    .AsQueryable();

                if (filtro.TipoId.HasValue && filtro.TipoId != 0)
                    res = res.Where(i => i.EventoTipoId == filtro.TipoId);

                if (filtro.ContratoId.HasValue && filtro.ContratoId != 0)
                    res = res.Where(i => i.ContratoId == filtro.ContratoId);

                if (filtro.ZonaId.HasValue && filtro.TipoId != 0)
                    res = res.Where(i => i.ZonaId == filtro.ZonaId);

                if (filtro.FrenteId.HasValue && filtro.ContratoId != 0)
                    res = res.Where(i => i.FrenteObraId == filtro.FrenteId);

                return await res.ToListAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task<int> CantidadEventos()
        {
            try
            {
                var result = await Context.Set<Evento>().CountAsync();
                return result;
            }
            catch (Exception) { throw; }
        }

        public async Task<EventoTipo> GetTipoEvento(int idTipoEvento)
        {
            try
            {
                var result = await Context.Set<EventoTipo>().AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.Id == idTipoEvento);

                return result;
            }
            catch (Exception) { throw; }
        }
    }
}
