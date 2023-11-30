using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IFrenteObraProfesionalesRepositorio : IRepositorio<FrenteObraProfesional>
    {
        Task<List<FrenteObraProfesional>> GetFrenteObraProfesionales();
        Task<FrenteObraProfesional> GetFrenteObraProfesionalfById(int id);
    }
}
