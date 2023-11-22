using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{

    [Index(nameof(CodTipo), Name = "EventoTipo_CodTipo_UQ", IsUnique = true)]
    public class EventoTipo : EntidadBase
    {
        [Required(ErrorMessage = "El código del tipo de Evento es obligatoria.")]
        [MaxLength(20, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodTipo { get; set; }

        [Required(ErrorMessage = "La descripción del Tipo de evento es obligatorio.")]
        [MaxLength(400, ErrorMessage = "Longitud Maxima de la descripción del Tipo de evento es de {1} caracteres")]
        public string DescTipo { get; set; }

        public List<Evento> Eventos { get; set; }
    }
}
