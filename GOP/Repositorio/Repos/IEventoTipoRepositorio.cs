using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IEventoTipoRepositorio : IRepositorio<EventoTipo>
    {
        Task<List<EventoTipo>> GetEventoTipos();
        Task<EventoTipo> GetEventoTipoById(int id);
    }
}
