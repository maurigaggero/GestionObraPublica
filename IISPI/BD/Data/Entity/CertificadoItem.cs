using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    [Index(nameof(CertificadoId), nameof(ItemContratoId), Name = "CertificadoItem_Certificado_ItemContrato_UQ", IsUnique = true)]
    public class CertificadoItem : EntidadBase
    {
        [Required(ErrorMessage = "El Certificado del Item es obligatorio.")]
        public int CertificadoId { get; set; }
        public Certificado Certificado { get; set; }

        [Required(ErrorMessage = "El Item del contrato al que corresponde el item el Certificado es obligatorio.")]
        public int ItemContratoId { get; set; }
        public ContratoItem ItemContrato { get; set; }

        [Required(ErrorMessage = "El código del Item es obligatorio.")]
        [MaxLength(5, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodItem { get; set; }

        [Required(ErrorMessage = "La descripción del Item es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción del Item es de {1} caracteres")]
        public string DescItem { get; set; }

        [Required(ErrorMessage = "La cantidad del Item del Contrato es obligatorio.")]
        public decimal CantidadTotal { get; set; }

        [Required(ErrorMessage = "La cantidad medida del item por IISPI es obligatoria.")]
        public decimal CantidadMedida { get; set; }

        [Required(ErrorMessage = "La cantidad informada por la Empresa es obligatoria.")]
        public decimal CantidadInformada { get; set; }

        [Required(ErrorMessage = "El coeficiente de incidencia es obligatorio.")]
        public decimal Coeficiente { get; set; }

        [Required(ErrorMessage = "El estado de ealuación es obligatorio.")]
        public bool Aceptado { get; set; }

        [Required(ErrorMessage = "La observación es obligatoria.")]
        public string Obs { get; set; }

        public List<CertificadoItemControl> CertificadoItemControls { get; set; }
    }
}
