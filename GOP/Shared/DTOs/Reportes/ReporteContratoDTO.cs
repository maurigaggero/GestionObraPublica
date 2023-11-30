using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ReporteContratoDTO
    {
        public string Expediente { get; set; }
        public string Zona { get; set; }
        public string Empresa { get; set; }
        public DateOnly Fecha { get; set; }
        public string ItemCodigo { get; set; }
        public string ItemDescripcion { get; set; }
        public string ItemUnidad { get; set; }
        public decimal ItemCantMedida { get; set; }
        public decimal ItemCantInformada { get; set; }
        public decimal ItemCantTotal { get; set; }
    }
}
