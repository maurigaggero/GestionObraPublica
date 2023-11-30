using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class FrenteObraDTO:DTOBase
    {
        [Required(ErrorMessage = "Se debe definir la zona a la que pertenece el frente de obra.")]
        public int ZonaId { get; set; }
        public ZonaDTO Zona { get; set; }

        [Required(ErrorMessage = "El código del Frente de Obra es obligatorio.")]
        [MaxLength(10, ErrorMessage = "Longitud Maxima del código del Frente de Obra es de {1} caracteres")]
        public string CodFrenteObra { get; set; }

        [Required(ErrorMessage = "El nombre del Frente de Obra es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima del nombre del Frente de Obra es de {1} caracteres")]
        public string NombreFrenteObra { get; set; }

        //public Point UbicacionFrente { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public List<FrenteObraProfesionalDTO> Profesionales { get; set; }
    }
}
