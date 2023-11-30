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
    public class ContratoEstructura : EntidadBase
    {
        [Required(ErrorMessage = "El Contrato al que pertenece esta cuadra es obligatorio.")]
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }

        [Required(ErrorMessage = "El tipo al que pertenece esta estructura es obligatorio.")]
        public int EstructuraTipoId { get; set; }
        public EstructuraTipo EstructuraTipo { get; set; }

        [Required(ErrorMessage = "El código de la Estructura es obligatorio.")]
        [MaxLength(4, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodEstructura { get; set; }

        [Required(ErrorMessage = "La descripción de la Estructura es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string DescEstructura { get; set; } = "";

        public decimal Longitud { get; set; }

        public decimal Ancho { get; set; }

        public int? CalleId { get; set; }
        public Calle Calle { get; set; }

        public int? EntreCalleId { get; set; }
        public Calle EntreCalle { get; set; }

        public int? YCalleId { get; set; }
        public Calle YCalle { get; set; }

        public int? EsquinaCalleId { get; set; }
        public Calle EsquinaCalle { get; set; }

        public Point UbicacionCentral { get; set; }
        public Point UbicacionInicio { get; set; }
        public Point UbicacionFin { get; set; }

        public List<ContratoEstructuraDoc> Documentos { get; set; }
        public List<CertificadoItem> CertificadoItems { get; set; }
    }
}
