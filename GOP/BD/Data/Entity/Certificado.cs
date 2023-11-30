using GOP.Shared.ENUM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data.Entity
{
    [Index(nameof(ContratoId), nameof(Periodo), Name = "Certificado_Periodo_UQ", IsUnique = true)]
    public class Certificado : EntidadBase
    {
        [Required(ErrorMessage = "El Contrato del certificado es obligatorio.")]
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }

        [Required(ErrorMessage = "El periodo del certificado es obligatorio (MMAAAA).")]
        [MaxLength(6, ErrorMessage = "Longitud maxima del periodo de certificado es de {1} caracteres, (MMAAAA)")]
        public string Periodo { get; set; }

        //Se carga cuando se aprueba o rechaza el certificado
        public DateTime FechaCertificado { get; set; }

        //Estado del certificado
        [Required(ErrorMessage = "El Estado del Certificado es obligatorio.")]
        public EnumEstadoCertificacion Estado { get; set; }

        //Se carga cuando se aprueba o rechaza el certificado
        public string Obs { get; set; } = "";

        //Se carga cuando se aprueba o rechaza el certificado
        public int? ZonaProfesionalId { get; set; }
        public ZonaProfesional ZonaProfesional { get; set; }

        //Se carga cuando se aprueba o rechaza el certificado
        public int? EmpresaProfesionalId { get; set; }
        public EmpresaProfesional EmpresaProfesional { get; set; }

        public List<CertificadoItem> CertificadoItems { get; set; }
        public List<Evento> Eventos { get; set; }
    }
}
