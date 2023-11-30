using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IContratoItemControlsRepositorio : IRepositorio<ContratoItemControl>
    {
        Task<List<ContratoItemControl>> GetContratoItemControls();
        Task<ContratoItemControl> GetContratoItemControlById(int id);
    }
}
