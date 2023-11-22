using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    [Index(nameof(CodigoZona), Name = "Zona_CodigoZona_UQ", IsUnique = true)]
    public class Zona : EntidadBase
    {
        [Required(ErrorMessage = "El código de la zona es obligatorio.")]
        [MaxLength(10,ErrorMessage = "Longitud Maxima del código de zona es de {1} caracteres")]
        public string CodigoZona { get; set; }
        [Required(ErrorMessage = "El nombre de la zona es obligatorio.")]
        [MaxLength(200,ErrorMessage = "Longitud Maxima del nombre de zona es de {1} caracteres")]
        public string NombreZona { get; set; }
        //[NotMapped]
        //public Point Centro { get; set; }
        public List<ZonaProfesional> Profesionales { get; set; }
        public List<Contrato> Contratos { get; set; }
        public List<FrenteObra> FrenteObras { get; set; }
        public List<Evento> Eventos { get; set; }
    }
}
