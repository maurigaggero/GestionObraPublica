using GOP.Shared.ENUM;
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
    //[Index(nameof(CertificadoItemId), nameof(ContratoItemControlId), nameof(Numero), Name = "CertificadoItemControl_CertificadoItem_CodControl_UQ", IsUnique = true)]
    public class CertificadoItemControl : EntidadBase
    {
        [Required(ErrorMessage = "El item del certificado que se controla es obligatorio.")]
        public int CertificadoItemId { get; set; }
        public CertificadoItem CertificadoItem { get; set; }

        [Required(ErrorMessage = "Es obligatorio El Item del contrato que se controla.")]
        public int ContratoItemControlId { get; set; }
        public ContratoItemControl ContratoItemControl { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string DescControl { get; set; }

        [Required(ErrorMessage = "La fecha del control del ítem es obligatoria.")]
        public DateTime FechaControl { get; set; }

        public Point UbicacionControl { get; set; }

        //Estado del certificado
        [Required(ErrorMessage = "El Estado del Certificado es obligatorio.")]
        public EnumEstadoControl Estado { get; set; }

        public string Obs { get; set; } = "";

        public List<CertificadoItemControlDoc> Documentos { get; set; }
        public List<CertificadoItemControlParam> Parametros { get; set; }
    }
}
