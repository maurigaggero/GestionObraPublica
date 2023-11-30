using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ReporteCertificadoItemDefDTO //Sergio
    {
        public string Expediente { get; set; }
        public string Zona { get; set; }
        public string Periodo { get; set; }
        public string Estado { get; set; }
        public string Fecha { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Un { get; set; }
        public decimal Coeficiente { get; set; }
        public decimal Total { get; set; }
        public decimal Ejecutado { get; set; }
        public decimal Total_Ejecutado { get; set; }
        public decimal Informado { get; set; }
        public decimal Total_Informado { get; set; }
        public string Observacion { get; set; }
    }
}
