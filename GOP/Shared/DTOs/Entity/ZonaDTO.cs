using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ZonaDTO : DTOBase
    {
        [Required(ErrorMessage = "El código de la zona es obligatorio.")]
        [MaxLength(10,ErrorMessage = "Longitud Maxima del código de zona es de {1} caracteres")]
        public string CodigoZona { get; set; }
        
        [Required(ErrorMessage = "El nombre de la zona es obligatorio.")]
        [MaxLength(200,ErrorMessage = "Longitud Maxima del nombre de zona es de {1} caracteres")]
        public string NombreZona { get; set; }

        //public Point UbicacionZona { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public List<ZonaProfesionalDTO> Profesionales { get; set; }
        public List<ContratoDTO> Contratos { get; set; }
        public List<FrenteObraDTO> FrenteObras { get; set; }
        public List<EventoDTO> Eventos { get; set; }
    }
}
