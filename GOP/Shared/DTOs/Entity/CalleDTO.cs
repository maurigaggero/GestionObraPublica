using GOP.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    //[Index(nameof(ContratoId), nameof(ItemId), nameof(CodItem), Name = "ContratoItem_Contrato_Item_CodItem_UQ", IsUnique = true)]
    public class CalleDTO : DTOBase
    {
        [Required(ErrorMessage = "La descripción del Item es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima del nombre de la calle es de {1} caracteres")]
        public string NombreCalle { get; set; }

        //public Point UbicacionCentral { get; set; }
        public double LatitudCentral { get; set; }
        public double LongitudCentral { get; set; }
        //public Point UbicacionInicio { get; set; }
        public double LatitudInicio { get; set; }
        public double LongitudInicio { get; set; }
        //public Point UbicacionFin { get; set; }
        public double LatitudFin { get; set; }
        public double LongitudFin { get; set; }
    }
}
