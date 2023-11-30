using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IItemDocRepositorio : IRepositorio<ItemDoc>
    {
        Task<List<ItemDoc>> GetItemDocs();
        Task<ItemDoc> GetItemDocById(int id);
    }
}
