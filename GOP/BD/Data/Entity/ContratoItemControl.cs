using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data.Entity
{
    [Index(nameof(ContratoItemId), nameof(ItemControlId), Name = "ContratoItemControl_ContratoItem_ItemControl_UQ", IsUnique = true)]
    [Index(nameof(ContratoItemId), nameof(CodControl), Name = "ContratoItemControl_ContratoItem_CodControl_UQ", IsUnique = true)]
    public class ContratoItemControl : EntidadBase
    {
        [Required(ErrorMessage = "Es obligatorio el Item del Contrato que controla.")]
        public int ContratoItemId { get; set; }
        public ContratoItem ContratoItem { get; set; }

        [Required(ErrorMessage = "El Parámetro de control del item es obligatorio.")]
        public int ItemControlId { get; set; }
        public ItemControl ItemControl { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        [MaxLength(5, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodControl { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string DescControl { get; set; }

        public List<ContratoItemControlParam> Parametros { get; set; }
        public List<ContratoItemControlDoc> Documentos { get; set; }
    }
}
