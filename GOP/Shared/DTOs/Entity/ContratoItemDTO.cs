using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ContratoItemDTO : DTOBase
    {
        [Required(ErrorMessage = "El Contrato al que pertenece este Item es obligatorio.")]
        public int ContratoId { get; set; }
        public ContratoDTO Contrato { get; set; }

        public int ItemId { get; set; }
        public List<int> IdsItems { get; set; }
        public ItemDTO Item { get; set; }

        public string CodItem { get; set; }

        public string DescItem { get; set; }

        public decimal CantidadTotal { get; set; }

        public decimal Coeficiente { get; set; }

        public List<ContratoItemControlDTO> ContratoItemControls { get; set; }
    }
}
