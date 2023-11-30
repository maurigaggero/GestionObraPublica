using GOP.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data
{
    public class EntidadBase : IEntidadBase
    {
        public int Id { get; set; }
        public string? Observacion { get; set; }
        public string IdCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public string IdModif { get; set; }
        public DateTime FechaModif { get; set; }
        public string IdBaja { get; set; }
        public DateTime FechaBaja { get; set; }
        public EnumEstadoRegistro EstadoRegistro { get; set; }
    }
}
