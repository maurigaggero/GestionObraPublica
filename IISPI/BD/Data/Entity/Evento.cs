using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    [Index(nameof(ContratoId), nameof(Numero), Name = "Certificado__UQ", IsUnique = true)]
    public class Evento : EntidadBase
    {
        [Required(ErrorMessage = "El número del evento es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Longitud Maxima del número del evento es de {1} caracteres")]
        public string Numero { get; set; }
        
        [Required(ErrorMessage = "La fecha del evento es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El Título del Evento es obligatorio.")]
        [MaxLength(250, ErrorMessage = "Longitud Maxima del Título es de {1} caracteres")]
        public string Título { get; set; }

        [Required(ErrorMessage = "La descripción del Evento es obligatorio.")]
        public string DescEvento { get; set; }

        [Required(ErrorMessage = "La unidad con que se mide el Item es obligatorio.")]
        public int EventoTipoId { get; set; }
        public EventoTipo Tipo { get; set; }

        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }

        public int CertificadoId { get; set; }
        public Certificado Certificado { get; set; }

        public int ZonaId { get; set; }
        public Zona Zona { get; set; }

        public int FrenteObraId { get; set; }
        public FrenteObra FrenteObra { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public List<EventoDoc> Documentos { get; set; }
        public List<EventoParam> Prametros { get; set; }
    }
}
