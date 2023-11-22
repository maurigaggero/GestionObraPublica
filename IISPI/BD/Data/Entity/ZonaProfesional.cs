﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    [Index(nameof(ZonaId), nameof(PersonaId), Name = "ZonaProfesional_Zona_Persona_FuncionProfesional_UQ", IsUnique = true)]
    public class ZonaProfesional: EntidadBase
    {
        [Required(ErrorMessage = "Falta identificar en que zona trabaja el profesional.")]
        public int ZonaId { get; set; }
        public Zona Zona { get; set; }

        [Required(ErrorMessage = "Falta identificar quien es el profesional.")]
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }

        [Required(ErrorMessage = "La descripción de la función que cumple un profesional es obligatorio.")]
        [MaxLength(150, ErrorMessage = "Longitud Maxima de la Función es de {1} caracteres")]
        public string DescFuncProf { get; set; }
    }
}
