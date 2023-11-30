using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data.Entity
{
    [Index(nameof(EventoId), nameof(EventoRelacionadoId), Name = "Evento_Evento_UQ", IsUnique = true)]
    public class EventoRelacionado : EntidadBase
    {
        [Required(ErrorMessage = "Falta identificar el evento a relacionar.")]
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        [Required(ErrorMessage = "Falta identificar el evento relacionado.")]
        public int EventoRelacionadoId { get; set; }
        public Evento EventoRelacion { get; set; }
    }
}
