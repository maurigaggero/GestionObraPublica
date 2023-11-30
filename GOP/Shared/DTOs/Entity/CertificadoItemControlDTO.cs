using GOP.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class CertificadoItemControlDTO : DTOBase
    {
        [Required(ErrorMessage = "El item del certificado que se controla es obligatorio.")]
        public int CertificadoItemId { get; set; }
        public CertificadoItemDTO CertificadoItem { get; set; }

        [Required(ErrorMessage = "Es obligatorio El Item del contrato que se controla.")]
        public int ContratoItemControlId { get; set; }
        public ContratoItemControlDTO ContratoItemControl { get; set; }

        [Required(ErrorMessage = "La fecha del Control es obligatoria.")]
        public DateTime FechaControl { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string DescControl { get; set; }

        //public Point UbicacionControl { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        //Estado del certificado
        [Required(ErrorMessage = "El Estado del Certificado es obligatorio.")]
        public EnumEstadoControl Estado { get; set; }

        public string Obs { get; set; } = "";

        public List<CertificadoItemControlDocDTO> Documentos { get; set; }
        public List<CertificadoItemControlParamDTO> Parametros { get; set; }
    }
}
