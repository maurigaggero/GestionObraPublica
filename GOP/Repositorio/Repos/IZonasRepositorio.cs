using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IZonasRepositorio : IRepositorio<Zona>
    {
        Task<List<Zona>> GetZonas();
        Task<Zona> GetZonaById(int id);
        Task<List<Zona>> GetZonasPorPersona(int idPersona);
        Task<Zona> GetZonaByIdNoFull(int id);
    }
}
