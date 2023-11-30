using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IZonasProfesionalesRepositorio : IRepositorio<ZonaProfesional>
    {
        Task<List<ZonaProfesional>> GetZonasProfesionales(int id);
        Task<ZonaProfesional> GetZonaProfesionalfById(int id);
    }
}
