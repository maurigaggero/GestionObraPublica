using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IEventoDocRepositorio : IRepositorio<EventoDoc>
    {
        Task<List<EventoDoc>> GetEventoDocs();
        Task<EventoDoc> GetEventoDocById(int id);
    }
}
