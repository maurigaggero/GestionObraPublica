using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data.Entity
{
    [Index(nameof(ItemId), nameof(CodControl), Name = "ItemControl_CodControl_UQ", IsUnique = true)]
    public class ItemControl : EntidadBase
    {
        [Required(ErrorMessage = "Es obligatorio El Item que controla.")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        [MaxLength(5, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodControl { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]

        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string DescControl{ get; set; }

        public List<ItemControlDoc> Documentos { get; set; }
        public List<ItemControlParam> Parametros { get; set; }
    }
}
