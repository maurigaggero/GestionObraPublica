using GOP.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class CertificadoDTO: DTOBase
    {
        [Required(ErrorMessage = "El Contrato del certificado es obligatorio.")]
        public int ContratoId { get; set; }
        public ContratoDTO Contrato { get; set; }

        [Required(ErrorMessage = "El periodo del certificado es obligatorio (MMAAAA).")]
        [MaxLength(6, ErrorMessage = "Longitud maxima del periodo de certificado es de {1} caracteres, (MMAAAA)")]
        public string Periodo { get; set; }

        //Se carga cuando se aprueba o rechaza el certificado
        public DateTime FechaCertificado { get; set; }

        //Estado del certificado
        public EnumEstadoCertificacion Estado { get; set; }

        //Se carga cuando se aprueba o rechaza el certificado
        public string Obs { get; set; } = "";

        //Se carga cuando se aprueba o rechaza el certificado
        public int? ZonaProfesionalId { get; set; }
        public ZonaProfesionalDTO ZonaProfesional { get; set; }

        //Se carga cuando se aprueba o rechaza el certificado
        public int? EmpresaProfesionalId { get; set; }
        public EmpresaProfesionalDTO EmpresaProfesional { get; set; }

        public List<CertificadoItemDTO> CertificadoItems { get; set; }
        public List<EventoDTO> Eventos { get; set; }

        //public static implicit operator CertificadoDTO(GOP.BD.Data.Entity.Certificado v)
        //{
        //    throw new NotImplementedException();
        //}

        //public static implicit operator CertificadoDTO(GOP.BD.Data.Entity.Certificado v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
