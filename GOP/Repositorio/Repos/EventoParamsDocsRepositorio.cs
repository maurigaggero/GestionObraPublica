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
    public class EventoParamDocsRepositorio : Repositorio<EventoParamDoc> , IEventoParamDocsRepositorio
    {
        public EventoParamDocsRepositorio(BDContext context) : base(context)
        {
        }
        public async Task<List<EventoParamDoc>> GetEventoParamDocs()
        {
            try
            {
                var list = await Context.Set<EventoParamDoc>()
                    .Include(i => i.EventoParam)
                    .Where(i => i.EstadoRegistro == 0).ToListAsync();
                return list;
            }
            catch (Exception) { throw; }
        }
        public async Task<EventoParamDoc> GetEventoParamDocById(int id)
        {
            try
            {
                var res = await Context.Set<EventoParamDoc>()
                                              .AsNoTracking()
                                              .Include(i => i.EventoParam)
                                              .FirstOrDefaultAsync(i => i.Id == id);
                return res;
            }
            catch (Exception) { throw; }
        }
    }
}
