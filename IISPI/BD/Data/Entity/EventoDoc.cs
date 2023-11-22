using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    public class EventoDoc : EntidadBase
    {
        [Required(ErrorMessage = "El Evento es obligatorio.")]
        public int EventolId { get; set; }
        public Evento Evento { get; set; }

        public string Descripcion { get; set; }

        [MaxLength(800, ErrorMessage = "Longitud Maxima del path es de {1} caracteres")]
        public string PathDoc { get; set; }
    }
}
