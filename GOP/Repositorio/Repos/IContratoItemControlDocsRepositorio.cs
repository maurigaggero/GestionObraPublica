using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IContratoItemControlDocsRepositorio : IRepositorio<ContratoItemControlDoc>
    {
        Task<List<ContratoItemControlDoc>> GetContratoItemControlDocs();
        Task<ContratoItemControlDoc> GetContratoItemControlDocById(int id);
    }
}
