using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IContratoDocRepositorio : IRepositorio<ContratoDoc>
    {
        Task<List<ContratoDoc>> GetContratoDocs();
        Task<ContratoDoc> GetContratoDocById(int id);
        Task<List<ContratoDoc>> GetContratoDocsByContratoId(int idContrato);
    }
}
