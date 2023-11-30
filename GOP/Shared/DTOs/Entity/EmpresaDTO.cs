using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class EmpresaDTO : DTOBase
    {
        [Required(ErrorMessage = "El CUIT es obligatorio.")]
        [MaxLength(11, ErrorMessage = "Longitud Maxima del CUIT es de {1} caracteres")]
        public string CUIT { get; set; }
        [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima del nombre de la empresa es de {1} caracteres")]
        public string Nombre { get; set; }

        //public Point Ubicacion {get; set;}
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public List<EmpresaProfesionalDTO> Profesionales { get; set; }
        public List<ContratoDTO> ContratoObras { get; set; }

        public List<EventoDTO> Eventos { get; set; }
    }
}
