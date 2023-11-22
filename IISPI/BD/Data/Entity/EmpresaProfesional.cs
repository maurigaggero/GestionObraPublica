using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    [Index(nameof(EmpresaId), nameof(PersonaId), Name = "EmpresaProfesional_Empresa_Persona_FuncionProfesional_UQ", IsUnique = true)]
    public class EmpresaProfesional : EntidadBase
    {
        [Required(ErrorMessage = "Falta identificar que empresa representa es el profesional.")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        [Required(ErrorMessage = "Falta identificar quien es el profesional.")]
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }

        [Required(ErrorMessage = "La descripción de la función que cumple un profesional es obligatorio.")]
        [MaxLength(150, ErrorMessage = "Longitud Maxima de la Función es de {1} caracteres")]
        public string DescFuncProf { get; set; }
    }
}
