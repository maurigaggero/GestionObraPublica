using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IItemsControlsDocsRepositorio : IRepositorio<ItemControlDoc>
    {
        Task<List<ItemControlDoc>> GetItemControls();
        Task<ItemControlDoc> GetItemControlById(int id);
    }
}
