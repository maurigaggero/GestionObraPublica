using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IContratoItemControlParamsRepositorio : IRepositorio<ContratoItemControlParam>
    {
        Task<List<ContratoItemControlParam>> GetContratoItemControlParams();
        Task<ContratoItemControlParam> GetContratoItemControlParamById(int id);
    }
}
