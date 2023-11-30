using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ItemControlDocDTO : DTOBase
    {
        [Required(ErrorMessage = "El Control es obligatorio.")]
        public int? ItemControlId { get; set; }
        public ItemControlDTO ItemControl { get; set; }

        public string Descripcion{ get; set; }
        public string PathDoc{ get; set; }

        [Required(ErrorMessage = "Seleccione un archivo.")]
        public string File { get; set; }
        public string Extension { get; set; }
        public string Carpeta { get; set; }
    }
}
