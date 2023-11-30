using GOP.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Identity
{
    public class RegistrarUserDTO
    {
        [Required(ErrorMessage = "La Persona es obligatoria.")]
        public int? IdPersona { get; set; }

        [Required(ErrorMessage = "La Contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La Contraseña debe tener como mínimo {1} caracteres.")]
        public string Contraseña { get; set; }
        [Required(ErrorMessage = "El Rol es obligatorio.")]
        public EnumRol Rol { get; set; }

        [Required(ErrorMessage = "Repita la contraseña.")]
        [MinLength(8, ErrorMessage = "La Contraseña debe tener como mínimo {1} caracteres.")]
        public string ContraseñaRepetida { get; set; }

    }
}
