using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ContratoItemControlDocDTO :DTOBase
    {
        [Required(ErrorMessage = "El control al que se adjunta el documento es obligatorio.")]
        public int ContratoItemControlId { get; set; }
        public ContratoItemControlDTO ContratoItemControl { get; set; }

        public int ItemControlDocId { get; set; }
        public ItemControlDocDTO ItemControlDoc { get; set; }
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Seleccione un archivo.")]
        public string File { get; set; }
        public string Extension { get; set; }
        public string Carpeta { get; set; }
        public string PathDoc { get; set; }
        public string Obs { get; set; } = "";

        //public Point UbicacionDoc { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
