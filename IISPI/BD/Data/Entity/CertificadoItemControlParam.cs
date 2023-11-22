using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    [Index(nameof(CertificadoItemControlId), nameof(Parametro), Name = "CertificadoItemControlParam_ItemControlId_Parametro_UQ", IsUnique = true)]
    public class CertificadoItemControlParam : EntidadBase
    {
        [Required(ErrorMessage = "El Control del certificado es obligatorio.")]
        public int CertificadoItemControlId { get; set; }
        public CertificadoItemControl CertificadoItemControl { get; set; }

        [Required(ErrorMessage = "El Parámetro es obligatorio.")]
        [MaxLength(10, ErrorMessage = "Longitud Maxima del parámetro es de {1} caracteres")]
        public string Parametro { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string Descripción { get; set; }

        [Required(ErrorMessage = "La unidad con que se mide el parámetro es obligatorio.")]
        public int UnidadId { get; set; }
        public Unidad Unidad { get; set; }

        [Required(ErrorMessage = "Valor del Parámetro medido en obra.")]
        public decimal ValorMedido { get; set; }

        [Required(ErrorMessage = "Valor Mínimo del Parámetro para ser aprobado.")]
        public decimal ValorMinimo { get; set; }

        [Required(ErrorMessage = "Valor Máximo del Parámetro para ser aprobado.")]
        public decimal ValorMaximo { get; set; }
    }
}
