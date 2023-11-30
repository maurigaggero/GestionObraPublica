using GOP.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacion)
        {
            return queryable
                .Skip((paginacion.Pagina - 1) * paginacion.RegistrosPorPagina)
                .Take(paginacion.RegistrosPorPagina);
        }
    }
}
