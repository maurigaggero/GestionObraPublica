using GOP.Shared.DTOs;
using GOP.Shared.DTOs.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ContratoEstructuraDTO : DTOBase
    {
        [Required(ErrorMessage = "El Contrato al que pertenece esta cuadra es obligatorio.")]
        public int ContratoId { get; set; }
        public ContratoDTO Contrato { get; set; }

        [Required(ErrorMessage = "La Estructura debe pertenecer a una calle.")]
        public int? CalleId { get; set; }
        public CalleDTO Calle { get; set; }

        [Required(ErrorMessage = "El tipo al que pertenece esta estructura es obligatorio.")]
        public int? EstructuraTipoId { get; set; }
        public EstructuraTipoDTO EstructuraTipo { get; set; }

        [Required(ErrorMessage = "El código de la Estructura es obligatorio.")]
        [MaxLength(4, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodEstructura { get; set; }

        [Required(ErrorMessage = "La descripción de la Estructura es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string DescEstructura { get; set; } = "";

        public decimal Longitud { get; set; }

        public decimal Ancho { get; set; }

        public int? EntreCalleId { get; set; }
        public CalleDTO EntreCalle { get; set; }

        public int? YCalleId { get; set; }
        public CalleDTO YCalle { get; set; }

        public int? EsquinaCalleId { get; set; }
        public CalleDTO EsquinaCalle { get; set; }

        //public Point UbicacionCentral { get; set; }
        public double LatitudCentral { get; set; }
        public double LongitudCentral { get; set; }
        //public Point UbicacionInicio { get; set; }
        public double LatitudInicio { get; set; }
        public double LongitudInicio { get; set; }
        //public Point UbicacionFin { get; set; }
        public double LatitudFin { get; set; }
        public double LongitudFin { get; set; }

        public List<ContratoEstructuraDocDTO> Documentos { get; set; }
        public List<CertificadoItemDTO> CertificadoItems { get; set; }
    }
}
