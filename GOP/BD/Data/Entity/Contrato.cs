using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data.Entity
{
    [Index(nameof(NumExpediente), Name = "Contrato_UQ", IsUnique = true)]
    public class Contrato : EntidadBase
    {
        [Required(ErrorMessage = "El Número de Expediente es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El Número de Expediente tiene como máximo {1} caracteres. Es sin puntosni símbolos.")]
        public string NumExpediente { get; set; }

        [Required(ErrorMessage = "El nombre de la carátula del Expediente es obligatorio.")]
        [MaxLength(300, ErrorMessage = "El nombre de la carátula como máximo {1} caracteres.")]
        public string Caratula { get; set; }

        [Required(ErrorMessage = "La descripción de la carátula del Expediente es obligatorio.")]
        [MaxLength(300, ErrorMessage = "La descripción de la carátula como máximo {1} caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Se debe definir la zona que abarca el contrato.")]
        public int ZonaId { get; set; }
        public Zona Zona { get; set; }

        [Required(ErrorMessage = "Se debe definir la Empresa que abarca el contrato.")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public List<ContratoItem> ContratoItems { get; set; }
        public List<Certificado> Certificados { get; set; }
        public List<Evento> Eventos { get; set; }
        public List<ContratoDoc> ContratoDocs { get; set; }
        public List<ContratoEstructura> ContratoEstructuras { get; set; }

    }
}
