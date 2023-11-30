using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IEventoParamDocsRepositorio : IRepositorio<EventoParamDoc>
    {
        Task<List<EventoParamDoc>> GetEventoParamDocs();
        Task<EventoParamDoc> GetEventoParamDocById(int id);
    }
}
