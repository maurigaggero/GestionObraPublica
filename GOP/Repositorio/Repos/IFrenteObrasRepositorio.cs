using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IFrenteObrasRepositorio : IRepositorio<FrenteObra>
    {
        Task<List<FrenteObra>> GetFrenteObras();
        Task<FrenteObra> GetFrenteObraById(int id);
        Task<List<FrenteObra>> GetFrenteObrasPorPersona(int idPersona);
        Task<List<FrenteObra>> GetFrenteObrasPorZona(int idZona);
        Task<FrenteObra> GetFrenteObraByIdNoFull(int id);
    }
}
