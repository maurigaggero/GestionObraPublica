using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ZonaProfesionalDTO: DTOBase
    {
        [Required(ErrorMessage = "Falta identificar en que zona trabaja el profesional.")]
        public int ZonaId { get; set; }
        public ZonaDTO Zona { get; set; }

        [Required(ErrorMessage = "Falta identificar quien es el profesional.")]
        public int? PersonaId { get; set; }
        public PersonaDTO Persona { get; set; }

        [Required(ErrorMessage = "La descripción de la función que cumple un profesional es obligatorio.")]
        [MaxLength(150, ErrorMessage = "Longitud Maxima de la Función es de {1} caracteres")]
        public string DescFuncProf { get; set; }
    }
}
