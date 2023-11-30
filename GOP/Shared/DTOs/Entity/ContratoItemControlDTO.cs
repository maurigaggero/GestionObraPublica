using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ContratoItemControlDTO : DTOBase
    {
        [Required(ErrorMessage = "Es obligatorio el Item del Contrato que controla.")]
        public int ContratoItemId { get; set; }
        public ContratoItemDTO ContratoItem { get; set; }

        [Required(ErrorMessage = "El Parámetro de control del item es obligatorio.")]
        public int ItemControlId { get; set; }
        public ItemControlDTO ItemControl { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        [MaxLength(5, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodControl { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string DescControl { get; set; }

        public List<ContratoItemControlParamDTO> Parametros { get; set; }
        public List<ContratoItemControlDocDTO> Documentos { get; set; }
    }
}
