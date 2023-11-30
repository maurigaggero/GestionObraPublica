using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ItemDTO : DTOBase
    {
        [Required(ErrorMessage = "El código del Item es obligatorio.")]
        [MaxLength(5, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodItem { get; set; }
       
        [Required(ErrorMessage = "La descripción del Item es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción del Item es de {1} caracteres")]
        public string DescItem { get; set; }

        [Required(ErrorMessage = "La unidad con que se mide el Item es obligatorio.")]
        public int? UnidadId { get; set; }
        public UnidadDTO Unidad { get; set; }

        public List<ItemControlDTO> ItemControles { get; set; }

    }
}
