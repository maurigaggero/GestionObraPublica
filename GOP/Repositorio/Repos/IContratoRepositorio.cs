using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IContratoRepositorio : IRepositorio<Contrato>
    {
        Task<List<Contrato>> GetContratos();
        Task<List<Contrato>> GetContratosAndItems();
        Task<Contrato> GetContratoById(int id);
        Task<Contrato> GetContratoByIdNoFull(int id);
        Task<string> GetCaratula(int id);  //sergio
    }
}
