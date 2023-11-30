using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Identity
{
    public class RespuestaAutenticacionDTO
    {
        public string Token { get; set; }
        public DateTime Expira { get; set; }
    }
}
