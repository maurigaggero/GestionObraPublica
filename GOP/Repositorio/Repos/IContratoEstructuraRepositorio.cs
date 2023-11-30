using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IContratoEstructuraDocRepositorio : IRepositorio<ContratoEstructuraDoc>
    {
        Task<List<ContratoEstructuraDoc>> GetContratoEstructuraDocs(int id);
        Task<ContratoEstructuraDoc> GetContratoEstructuraDocById(int id);
    }
}
