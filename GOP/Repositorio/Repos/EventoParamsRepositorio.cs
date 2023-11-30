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
    public class EventoParamsRepositorio : Repositorio<EventoParam> , IEventoParamsRepositorio
    {
        public EventoParamsRepositorio(BDContext context) : base(context)
        {
        }
        public async Task<List<EventoParam>> GetEventoParams()
        {
            try
            {
                var list = await Context.Set<EventoParam>()
                    .Include(i => i.Unidad)
                    .Include(i => i.Evento)
                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
        public async Task<EventoParam> GetEventoParamById(int id)
        {
            try
            {
                var res = await Context.Set<EventoParam>()
                                              .AsNoTracking()
                                              .Include(i => i.Unidad)
                                              .Include(i => i.Evento)
                                              .FirstOrDefaultAsync(i => i.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }
    }
}
