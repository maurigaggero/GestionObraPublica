using GOP.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data.Entity
{
    public class CertificadoItemDef : EntidadBase
    {
        [Required(ErrorMessage = "El Certificado es obligatorio")]
        public int CertificadoId { get; set; }
        public Certificado Certificado { get; set; }
        
        [Required(ErrorMessage = "El Contrato del Item es obligatorio")]
        public int ItemContratoId { get; set; }
        public ContratoItem ItemContrato { get; set; }
        
        [Required(ErrorMessage = "El Codigo del Item es obligatorio")]
        public string CodItem { get; set; }
        
        [Required(ErrorMessage = "La Descripcion es obligatorio")]
        public string DescItem { get; set; }
        
        [Required(ErrorMessage = "La Unidad es obligatorio")]
        public int UnidadId { get; set; }
        public Unidad Unidad { get; set; }
        
        [Required(ErrorMessage = "La Cantidad Total del Contrato para el item es obligatorio")]
        public decimal CantidadTotal { get; set; }

        [Required(ErrorMessage = "La Cantidad Informada por la inspección es obligatorio")]
        public decimal CantidadInformada { get; set; }

        [Required(ErrorMessage = "La Cantidad Total Informada por la inspección del Contrato para el item es obligatorio")]
        public decimal CantidadTotalInformada { get; set; }

        [Required(ErrorMessage = "La Cantidad Acumulada según avance del Certificado es obligatorio")]
        public decimal CantidadAcumulada { get; set; }

        [Required(ErrorMessage = "La Cantidad Total Ejecutada del Contrato para el item es obligatorio")]
        public decimal CantidadTotalEjecutado { get; set; }

        [Required(ErrorMessage = "El Coeficiente del Certificado es obligatorio")]
        public decimal Coeficiente { get; set; }
        [Required(ErrorMessage = "El Estado del Certificado es obligatorio")]
        public EnumEstadoCertificacion Estado { get; set; }
        
        public string Obs { get; set; }
    }
}
