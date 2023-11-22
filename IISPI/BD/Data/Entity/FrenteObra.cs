using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    [Index(nameof(ZonaId), nameof(CodFrenteObra), Name = "FrenteObra_Zona_CodFrenteObra_UQ", IsUnique = true)]
    public class FrenteObra:EntidadBase
    {
        [Required(ErrorMessage = "Se debe definir la zona a la que pertenece el frente de obra.")]
        public int ZonaId { get; set; }
        public Zona Zona { get; set; }

        [Required(ErrorMessage = "El código del Frente de Obra es obligatorio.")]
        [MaxLength(10, ErrorMessage = "Longitud Maxima del código del Frente de Obra es de {1} caracteres")]
        public string CodFrenteObra { get; set; }

        [Required(ErrorMessage = "El nombre del Frente de Obra es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima del nombre del Frente de Obra es de {1} caracteres")]
        public string NombreFrenteObra { get; set; }

        //[NotMapped]
        //public Point Centro { get; set; }
        public List<FrenteObraProfesional> Profesionales { get; set; }
    }
}
