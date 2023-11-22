using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.Shared.DTOs.IINSPIDtos
{
    public class ItemsControlsDTO
    {
        [Required(ErrorMessage = "Es obligatorio El Item que controla.")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        [MaxLength(5, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodControl { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]

        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string DescControl { get; set; }
    }
}
