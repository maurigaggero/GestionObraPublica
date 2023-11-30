using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data.Entity
{
    //[Index(nameof(ContratoId), nameof(ItemId), nameof(CodItem), Name = "ContratoItem_Contrato_Item_CodItem_UQ", IsUnique = true)]
    public class Calle : EntidadBase
    {
        [Required(ErrorMessage = "La descripción del Item es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima del nombre de la calle es de {1} caracteres")]
        public string NombreCalle { get; set; }

        public Point UbicacionCentral { get; set; }
        public Point UbicacionInicio { get; set; }
        public Point UbicacionFin { get; set; }
    }
}
