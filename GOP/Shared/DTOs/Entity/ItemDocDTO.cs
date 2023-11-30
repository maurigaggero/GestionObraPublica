using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ItemDocDTO : DTOBase
    {
        [Required(ErrorMessage = "El Item al que se adjunta el documento es obligatorio.")]
        public int ItemId { get; set; }
        public ItemDTO Item { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria.")]
        [MaxLength(800, ErrorMessage = "Longitud Maxima de la descripción del documento es de {1} caracteres")]
        public string Descripcion { get; set; } = "";

        public string PathDoc { get; set; }

        public string File { get; set; }

        public string Extension { get; set; }

        //public Point UbicacionDoc { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public string Obs { get; set; } = "";
    }
}
