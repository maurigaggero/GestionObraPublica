using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ReporteCertificadoDTO //Sergio
    {
        public string Expediente { get; set; }
        public string Zona { get; set; }
        public string Periodo { get; set; }
        public string Fecha { get; set; }
        public string Estado_Certificado { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Un { get; set; }
        public string Medicion { get; set; }
        public decimal Medido { get; set; }
        public decimal Informado { get; set; }
        public decimal Total { get; set; }
        public string Frente { get; set; }
        public string Estructura { get; set; }
        public string Estado_Medicion { get; set; }
        public string Observacion { get; set; }
    }
}
