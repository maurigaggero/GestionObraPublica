using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data.Entity
{
    [Index(nameof(EventoId), nameof(Parametro), Name = "EventoParam_EventoId_Parametro_UQ", IsUnique = true)]
    public class EventoParam : EntidadBase
    {
        [Required(ErrorMessage = "El Evento es obligatorio.")]
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        [Required(ErrorMessage = "El Nombre del Parámetro es obligatorio.")]
        [MaxLength(10, ErrorMessage = "Longitud Maxima del parámetro es de {1} caracteres")]
        public string Parametro { get; set; }

        [Required(ErrorMessage = "La descripción del Parámetro es obligatoria.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string Descripción { get; set; }

        [Required(ErrorMessage = "La unidad con que se mide el Parámetro es obligatorio.")]
        public int UnidadId { get; set; }
        public Unidad Unidad { get; set; }

        [Required(ErrorMessage = "Valor del Parámetro medido en obra.")]
        public decimal ValorMedido { get; set; }

        public Point UbicacionDoc { get; set; }
        public string Obs { get; set; }

    }
}
