using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs
{
    public class FiltroEventoReporteDTO
    {
        public int? TipoId { get; set; }

        public string FechaDesde { get; set; }

        public string FechaHasta { get; set; }

        public int? ContratoId {get;set;}
        public int? ZonaId {get;set;}
        public int? FrenteId {get;set;}
    }
}
