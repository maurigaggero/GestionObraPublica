using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ContratoDTO : DTOBase
    {
        [Required(ErrorMessage = "El Número de Expediente es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El Número de Expediente tiene como máximo {1} caracteres. Es sin puntos ni símbolos.")]
        public string NumExpediente { get; set; }

        [Required(ErrorMessage = "El nombre de la carátula del Expediente es obligatorio.")]
        [MaxLength(300, ErrorMessage = "El nombre de la carátula como máximo {1} caracteres.")]
        public string Caratula { get; set; }

        [Required(ErrorMessage = "La descripción de la carátula del Expediente es obligatorio.")]
        [MaxLength(300, ErrorMessage = "La descripción de la carátula como máximo {1} caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Se debe definir la zona que abarca el contrato.")]
        public int? ZonaId { get; set; }
        public ZonaDTO Zona { get; set; }

        [Required(ErrorMessage = "Se debe definir la Empresa que abarca el contrato.")]
        public int? EmpresaId { get; set; }
        public EmpresaDTO Empresa { get; set; }

        public List<ContratoItemDTO> ContratoItems { get; set; }
        public List<CertificadoDTO> Certificados { get; set; }
        public List<EventoDTO> Eventos { get; set; }
        public List<ContratoEstructuraDTO> ContratoEstructuras { get; set; }

        public List<ContratoDocDTO> ContratoDocs { get; set; }  

    }
}
