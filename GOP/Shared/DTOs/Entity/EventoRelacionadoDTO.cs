using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class EventoRelacionadoDTO : DTOBase
    {
        [Required(ErrorMessage = "Falta identificar el evento a relacionar.")]
        public int EventoId { get; set; }
        public EventoDTO Evento { get; set; }

        [Required(ErrorMessage = "Falta identificar el evento relacionado.")]
        public int EventoRelacionadoId { get; set; }
        public EventoDTO EventoRelacionado { get; set; }
    }
}
