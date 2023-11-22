using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    [Index(nameof(ContratoId), nameof(ItemId), nameof(CodItem), Name = "ContratoItem_Contrato_Item_CodItem_UQ", IsUnique = true)]
    public class ContratoItem : EntidadBase
    {
        [Required(ErrorMessage = "El Contrato al que pertenece este Item es obligatorio.")]
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }

        [Required(ErrorMessage = "El Item del Contrato es obligatorio.")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Required(ErrorMessage = "El código del Item es obligatorio.")]
        [MaxLength(5, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodItem { get; set; }

        [Required(ErrorMessage = "La descripción del Item es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción del Item es de {1} caracteres")]
        public string DescItem { get; set; }

        [Required(ErrorMessage = "La cantidad del Item del Contrato es obligatorio.")]
        public decimal CantidadTotal { get; set; }

        [Required(ErrorMessage = "El coeficiente de incidencia es obligatorio.")]
        public decimal Coeficiente { get; set; }

        public List<ContratoItemControl> ContratoItemControls { get; set; }
    }
}
