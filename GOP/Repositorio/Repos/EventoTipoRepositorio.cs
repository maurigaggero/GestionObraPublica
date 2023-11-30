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
    public class EventoTipoRepositorio : Repositorio<EventoTipo>, IEventoTipoRepositorio
    {
        public EventoTipoRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<EventoTipo> GetEventoTipoById(int id)
        {
            try
            {
                var res = await Context.Set<EventoTipo>()
                                    .AsNoTracking()
                                    .Include(e => e.Eventos)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<EventoTipo>> GetEventoTipos()
        {
            try
            {
                //var list = await Context.Set<EventoTipo>()
                //                    .Include(e => e.Eventos)
                //                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                var list = await Context.Set<EventoTipo>()
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
