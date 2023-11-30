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
    public class EventoDocRepositorio : Repositorio<EventoDoc>, IEventoDocRepositorio
    {
        public EventoDocRepositorio(BDContext context) : base(context)
        {
        }

        public async Task<EventoDoc> GetEventoDocById(int id)
        {
            try
            {
                var res = await Context.Set<EventoDoc>()
                                    .AsNoTracking()
                                    .Include(e => e.Evento)
                                    .FirstOrDefaultAsync(e => e.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<EventoDoc>> GetEventoDocs()
        {
            try
            {
                var list = await Context.Set<EventoDoc>()
                                    .Include(e => e.Evento)
                                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
    }
}
