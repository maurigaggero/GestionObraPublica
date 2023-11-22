using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.Shared.DTOs.IINSPIDtos
{
    public class UnidadesDTO
    {
        [Required(ErrorMessage = "El código de la unidad es obligatoria.")]
        [MaxLength(20, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodUnidad { get; set; }

        [Required(ErrorMessage = "La descripción de la Unidad es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción de la Unidad es de {1} caracteres")]
        public string DescUnidad { get; set; }
    }
}
