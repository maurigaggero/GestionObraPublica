using GOP.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class CertificadoItemDefDTO : DTOBase
    {
        [Required(ErrorMessage = "El Certificado es obligatorio")]
        public int CertificadoId { get; set; }
        public CertificadoDTO Certificado { get; set; }
        [Required(ErrorMessage = "El Contrato del Item es obligatorio")]
        public int ItemContratoId { get; set; }
        public ContratoItemDTO ItemContrato { get; set; }
        public string CodItem { get; set; }
        public string DescItem { get; set; }
        public int UnidadId { get; set; }
        public UnidadDTO Unidad { get; set; }
        public decimal CantidadTotal { get; set; }
        public decimal CantidadInformada { get; set; }
        public decimal CantidadTotalInformada { get; set; }
        public decimal CantidadAcumulada { get; set; }
        public decimal CantidadTotalEjecutado { get; set; }
        public decimal Coeficiente { get; set; }
        public EnumEstadoCertificacion Estado { get; set; }
        public string Obs { get; set; }
    }
}
