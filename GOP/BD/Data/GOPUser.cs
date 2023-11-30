using GOP.BD.Data.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.BD.Data
{
    public class GOPUser:IdentityUser
    {
        public int? PersonaId { get; set; }
        public Persona Persona { get; set; }
    }
}
