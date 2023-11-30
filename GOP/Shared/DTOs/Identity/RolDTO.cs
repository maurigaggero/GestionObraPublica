using GOP.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Identity
{
    public class RolDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public EnumRol Rol { get; set; }
    }
}
