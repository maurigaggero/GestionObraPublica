using GOP.BD.Data.Entity;
using GOP.Shared.DTOs;
using GOP.Shared.DTOs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IEventoRepositorio : IRepositorio<Evento>
    {
        Task<List<Evento>> GetEventos(FiltroEventoDTO filtro);
        Task<Evento> GetEventoById(int id);
        Task<int> CantidadEventos();
        Task<EventoTipo> GetTipoEvento(int idTipoEvento);
        Task<List<Evento>> GetReporteEventos(FiltroEventoReporteDTO filtro);
    }
}
