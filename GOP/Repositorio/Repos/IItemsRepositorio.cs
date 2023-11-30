using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IItemsRepositorio : IRepositorio<Item>
    {
        Task<List<Item>> GetItems();
        Task<Item> GetItemById(int id);
    }
}
