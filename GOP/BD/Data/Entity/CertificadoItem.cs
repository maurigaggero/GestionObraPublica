using NetTopologySuite.Geometries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOP.Shared.ENUM;

namespace GOP.BD.Data.Entity
{
    //[Index(nameof(CertificadoId), nameof(ItemContratoId), Name = "CertificadoItem_Certificado_ItemContrato_UQ", IsUnique = true)]
    public class CertificadoItem : EntidadBase
    {
        [Required(ErrorMessage = "El Certificado del Item es obligatorio.")]
        public int CertificadoId { get; set; }
        public Certificado Certificado { get; set; }

        [Required(ErrorMessage = "El Item del contrato al que corresponde el item el Certificado es obligatorio.")]
        public int ItemContratoId { get; set; }
        public ContratoItem ItemContrato { get; set; }

        [Required(ErrorMessage = "El Frente de Obra de la medición del Ítem es obligatorio.")]
        public int FrenteObraId { get; set; }
        public FrenteObra FrenteObra { get; set; }

        public int? ContratoEstructuraId { get; set; }
        public ContratoEstructura ContratoEstructura { get; set; }

        [Required(ErrorMessage = "El código del Item es obligatorio.")]
        [MaxLength(5, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodItem { get; set; }

        [Required(ErrorMessage = "La descripción del Item es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción del Item es de {1} caracteres")]
        public string DescItem { get; set; }

        [Required(ErrorMessage = "La unidad con que se mide el Item es obligatorio.")]
        public int UnidadId { get; set; }
        public Unidad Unidad { get; set; }

        [Required(ErrorMessage = "La fecha de medición del ítem es obligatoria.")]
        public DateTime FechaMedicion { get; set; }

        public Point Ubicacion { get; set; }

        [Required(ErrorMessage = "La cantidad del Item del Contrato es obligatorio.")]
        public decimal CantidadTotal { get; set; }

        [Required(ErrorMessage = "La cantidad medida del item por GOP es obligatoria.")]
        public decimal CantidadMedida { get; set; }

        [Required(ErrorMessage = "La cantidad informada por la Empresa es obligatoria.")]
        public decimal CantidadInformada { get; set; }

        [Required(ErrorMessage = "El coeficiente de incidencia es obligatorio.")]
        public decimal Coeficiente { get; set; }

        //Estado del certificado
        [Required(ErrorMessage = "El Estado del Certificado es obligatorio.")]
        public EnumEstadoCertificacion Estado { get; set; }

        //Se carga cuando se genera el certificado definitivo
        public int? CertificadoItemDefId { get; set; }
        public CertificadoItemDef CertificadoItemDef { get; set; }

        public string Obs { get; set; } = "";

        public List<CertificadoItemControl> CertificadoItemControls { get; set; }
    }
}
