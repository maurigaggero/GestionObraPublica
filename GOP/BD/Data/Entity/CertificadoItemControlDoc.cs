﻿using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data.Entity
{
    public class CertificadoItemControlDoc : EntidadBase
    {
        [Required(ErrorMessage = "El control al que se adjunta el documento es obligatorio.")]
        public int CertificadoItemControlId { get; set; }
        public CertificadoItemControl CertificadoItemControl { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria.")]
        [MaxLength(800, ErrorMessage = "Longitud Maxima de la descripción del documento es de {1} caracteres")]
        public string Descripcion { get; set; } = "";

        [Required(ErrorMessage = "El Nombre del Documento es obligatorio.")]
        [MaxLength(300, ErrorMessage = "Longitud Maxima del Nombre del Documento es de {1} caracteres")]
        public string NomDoc { get; set; } = "";

        [Required(ErrorMessage = "El Path del docuemnto es obligatorio.")]
        [MaxLength(800, ErrorMessage = "Longitud Maxima del path es de {1} caracteres")]
        public string PathDoc { get; set; }

        public Point UbicacionDoc { get; set; }

        public string Obs { get; set; } = "";
    }
}
