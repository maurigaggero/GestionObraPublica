using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IItemsControlsParamsRepositorio : IRepositorio<ItemControlParam>
    {
        Task<List<ItemControlParam>> GetItemControlParams();
        Task<ItemControlParam> GetItemControlParamById(int id);
    }
}
