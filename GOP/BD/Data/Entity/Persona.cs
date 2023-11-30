using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GOP.BD.Data.Entity
{
    [Index(nameof(DNI), Name = "PersonaDNI_UQ", IsUnique = true)]
    public class Persona : EntidadBase
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
        [EmailAddress(ErrorMessage = "Formato incorrecto de mail")]
        [MaxLength(300, ErrorMessage = "El Mail puede tener como máximo {1} caracteres.")]
        public string Email { get; set; } = "";

        [Phone(ErrorMessage = "Formato incorrecto de numeración de telefono")]
        [MaxLength(20, ErrorMessage = "El Número de Teléfono/celular puede tener como máximo {1} caracteres.")]
        public string Telefono { get; set; } = "";
    }
}
