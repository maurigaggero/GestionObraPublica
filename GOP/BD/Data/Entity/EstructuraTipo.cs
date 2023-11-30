using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data.Entity
{

    [Index(nameof(CodTipo), Name = "EstructuraTipo_CodTipo_UQ", IsUnique = true)]
    public class EstructuraTipo : EntidadBase
    {
        [Required(ErrorMessage = "El código del tipo de Estructura es obligatoria.")]
        [MaxLength(2, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodTipo { get; set; }

        [Required(ErrorMessage = "La descripción del Tipo de Estructura es obligatorio.")]
        [MaxLength(400, ErrorMessage = "Longitud Maxima de la descripción del Tipo de Estructura es de {1} caracteres")]
        public string DescTipo { get; set; }

        public List<ContratoEstructura> ContratoEstructuras { get; set; }
    }
}
