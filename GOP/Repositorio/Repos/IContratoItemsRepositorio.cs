using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IContratoItemsRepositorio : IRepositorio<ContratoItem>
    {
        Task<List<ContratoItem>> GetContratoItems();
        Task<ContratoItem> GetContratoItemById(int id);
        Task<List<ContratoItem>> GetContratoItemsByContratoId(int idContrato);

        //llama a los items para dar el alta masiva de los contrato items
        //y evitar inyeccion en controller
        Task<Item> GetItemDelContrato(int id); 
    }
}
