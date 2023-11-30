using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data.Entity
{
    [Index(nameof(Parametro), Name = "ParamCatalog_Parametro_UQ", IsUnique = true)]
    public class ParamCatalog : EntidadBase
    {
        [Required(ErrorMessage = "El Parámetro es obligatorio.")]
        [MaxLength(10, ErrorMessage = "Longitud Maxima del parámetro es de {1} caracteres")]
        public string Parametro { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string Descripción { get; set; }

        [Required(ErrorMessage = "La unidad con que se mide el parámetro es obligatorio.")]
        public int UnidadId { get; set; }
        public Unidad Unidad { get; set; }

        public decimal ValorMinimo { get; set; }

        public decimal ValorMaximo { get; set; }
    }
}
