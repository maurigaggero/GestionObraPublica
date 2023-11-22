using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    [Index(nameof(ContratoId),nameof(Numero), Name = "Certificado__UQ", IsUnique = true)]
    public class Certificado: EntidadBase
    {
        [Required(ErrorMessage = "El Contrato del certificado es obligatorio.")]
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }

        [Required(ErrorMessage = "El número de certificado es obligatorio.")]
        [MaxLength(10, ErrorMessage = "Longitud maxima del número de certificado es de {1} caracteres")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "La fecha de certificación es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El profesional que elaboró el certificado es obligatorio.")]
        public int ZonaProfesionalId { get; set; }

        public ZonaProfesional ZonaProfesional { get; set; }
        public List<CertificadoItem> CertificadoItems { get; set; }
        public List<Evento> Eventos { get; set; }
    }
}
