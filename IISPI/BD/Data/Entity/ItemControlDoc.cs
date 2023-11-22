using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    public class ItemControlDoc : EntidadBase
    {
        [Required(ErrorMessage = "El Control es obligatorio.")]
        public int ItemControlId { get; set; }
        public ItemControl ItemControl { get; set; }

        public string Descripcion{ get; set; }

        [MaxLength(800, ErrorMessage = "Longitud Maxima del path es de {1} caracteres")]
        public string PathDoc { get; set; }
    }
}
