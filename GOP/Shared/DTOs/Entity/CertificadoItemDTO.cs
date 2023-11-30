using GOP.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class CertificadoItemDTO : DTOBase
    {

        [Required(ErrorMessage = "El Certificado del Item es obligatorio.")]
        public int CertificadoId { get; set; }
        public CertificadoDTO Certificado { get; set; }

        [Required(ErrorMessage = "El Item del contrato al que corresponde el item el Certificado es obligatorio.")]
        public int ItemContratoId { get; set; }
        public ContratoItemDTO ItemContrato { get; set; }

        [Required(ErrorMessage = "El Frente de Obra de la medición del Ítem es obligatorio.")]
        public int FrenteObraId { get; set; }
        public FrenteObraDTO FrenteObra { get; set; }

        public int? ContratoEstructuraId { get; set; }
        public ContratoEstructuraDTO ContratoEstructura { get; set; }

        [Required(ErrorMessage = "El código del Item es obligatorio.")]
        [MaxLength(5, ErrorMessage = "Longitud Maxima del código es de {1} caracteres")]
        public string CodItem { get; set; }

        [Required(ErrorMessage = "La descripción del Item es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción del Item es de {1} caracteres")]
        public string DescItem { get; set; }

        [Required(ErrorMessage = "La unidad con que se mide el Item es obligatorio.")]
        public int UnidadId { get; set; }
        public UnidadDTO Unidad { get; set; }

        [Required(ErrorMessage = "La fecha de medición del ítem es obligatoria.")]
        public DateTime FechaMedicion { get; set; }

        //public Point Ubicacion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

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
        public CertificadoItemDefDTO CertificadoItemDef { get; set; }

        public string Obs { get; set; } = "";

        public List<CertificadoItemControlDTO> CertificadoItemControls { get; set; }
    }
}
