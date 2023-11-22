using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.Shared.DTOs.Identity
{
    public class RespuestaAutenticacionDTO
    {
        public string Token { get; set; }
        public DateTime Expira { get; set; }
    }
}
