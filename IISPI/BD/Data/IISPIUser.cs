using IISPI.BD.Data.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISPI.BD.Data
{
    public class IISPIUser:IdentityUser
    {
        public int? PersonaId { get; set; }
        public Persona Persona { get; set; }
    }
}
