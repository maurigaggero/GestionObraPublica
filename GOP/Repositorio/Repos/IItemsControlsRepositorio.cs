using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IItemsControlsRepositorio : IRepositorio<ItemControl>
    {
        Task<List<ItemControl>> GetItemControls();
        Task<ItemControl> GetItemControlById(int id);
        Task<List<ItemControl>> GetItemControlsByItem(int idItem);
    }
}
