using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.Shared.DTOs.Identity
{
    public class RegistrarUserDTO:CredencialUserDTO
    {
        [Required(ErrorMessage = "El DNI de la Persona es obligatorio.")]
        [MaxLength(8, ErrorMessage = "El DNI de la Persona tiene como máximo {1} números. Es sin puntos.")]
        public string DNI { get; set; }
        [Required(ErrorMessage = "El Nombre de la Persona es obligatorio.")]
        [MaxLength(200, ErrorMessage = "El Nombre de la Persona tiene como máximo {1} caracteres.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Nombre de la Persona es obligatorio.")]
        [MaxLength(200, ErrorMessage = "El Nombre de la Persona tiene como máximo {1} caracteres.")]
        public string Apellido { get; set; }

    }
}
