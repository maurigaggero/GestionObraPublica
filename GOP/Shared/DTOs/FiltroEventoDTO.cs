using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class FiltroEventoDTO : PaginacionDTO
    {
        public int? TipoId { get; set; }

        public string FechaDesde { get; set; }

        public string FechaHasta { get; set; }

        public int? ContratoId {get;set;}
    }
}
