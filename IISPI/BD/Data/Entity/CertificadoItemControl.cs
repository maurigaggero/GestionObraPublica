using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    [Index(nameof(CertificadoItemId), nameof(ContratoItemControlId), nameof(Numero), Name = "CertificadoItemControl_CertificadoItem_CodControl_UQ", IsUnique = true)]
    public class CertificadoItemControl : EntidadBase
    {
        [Required(ErrorMessage = "El item del certificado que se controla es obligatorio.")]
        public int CertificadoItemId { get; set; }
        public CertificadoItem CertificadoItem { get; set; }

        [Required(ErrorMessage = "Es obligatorio El Item del contrato que se controla.")]
        public int ContratoItemControlId { get; set; }
        public ContratoItemControl ContratoItemControl { get; set; }

        [Required(ErrorMessage = "El número es obligatorio.")]
        [MaxLength(8, ErrorMessage = "Longitud Maxima del Número es de {1} caracteres")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "La fecha del Control es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string DescControl { get; set; }

        [Required]
        public bool Aceptado { get; set; }

        [Required(ErrorMessage = "La observación es obligatoria.")]
        public string Obs { get; set; }

        public List<CertificadoItemControlDoc> Documentos { get; set; }
        public List<CertificadoItemControlParam> Parametros { get; set; }
    }
}
