using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    [Index(nameof(CUIT), Name = "Empresal_CUIT_UQ", IsUnique = true)]
    public class Empresa : EntidadBase
    {
        [Required(ErrorMessage = "El CUIT es obligatorio.")]
        [MaxLength(11, ErrorMessage = "Longitud Maxima del CUIT es de {1} caracteres")]
        public string CUIT { get; set; }
        [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima del nombre de la empresa es de {1} caracteres")]
        public string Nombre { get; set; }
        //public Point Ubicacion { get; set; }

        public List<EmpresaProfesional> Profesionales { get; set; }
        public List<Contrato> ContratoObras { get; set; }

        public List<Evento> Eventos { get; set; }
    }
}
