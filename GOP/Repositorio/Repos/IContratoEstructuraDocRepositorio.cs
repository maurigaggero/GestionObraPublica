using GOP.BD.Data.Entity;
using GOP.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IContratoEstructuraRepositorio : IRepositorio<ContratoEstructura>
    {
        Task<List<ContratoEstructura>> GetContratoEstructuras(int contratoId);
        Task<List<ContratoEstructura>> GetContratoEstructurasByContratoId(PaginacionDTO paginacion, int id);
        Task<ContratoEstructura> GetContratoEstructuraById(int id);

    }
}
