using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data.Entity
{
    public class CertificadoItemControlDoc : EntidadBase
    {
        [Required(ErrorMessage = "El control al que se adjunta el documento es obligatorio.")]
        public int CertificadoItemControlId { get; set; }
        public CertificadoItemControl CertificadoItemControl { get; set; }

        public string Descripcion { get; set; }

        [MaxLength(800, ErrorMessage = "Longitud Maxima del path es de {1} caracteres")]
        public string PathDoc { get; set; }
    }
}
