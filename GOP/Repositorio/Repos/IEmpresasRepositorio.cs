using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IEmpresasRepositorio : IRepositorio<Empresa>
    {
        Task<List<Empresa>> GetEmpresas();
        Task<Empresa> GetEmpresaById(int id);
    }
}
