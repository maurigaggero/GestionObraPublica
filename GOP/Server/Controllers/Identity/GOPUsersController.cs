using AutoMapper;
using GOP.BD.Data;
using GOP.BD.Data.Entity;
using GOP.Repositorio.Repos;
using GOP.Shared.DTOs.Entity;
using GOP.Shared.DTOs.Identity;
using GOP.Shared.ENUM;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GOP.Server.Controllers.Identity
{
    [ApiController]
    [Route("GOP/api/user")]
    public class GOPUsersController : ControllerBase
    {
        const double minexpiracion = 60;
        private readonly UserManager<GOPUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<GOPUser> signInManager;
        private readonly IPersonasRepositorio reposPersona;
        private readonly IMapper mapper;

        public GOPUsersController(UserManager<GOPUser> userManager,
                                    IConfiguration configuration,
                                    SignInManager<GOPUser> signInManager,
                                    IPersonasRepositorio reposPersona,
                                    IMapper mapper)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.reposPersona = reposPersona;
            this.mapper = mapper;
        }

        #region EndPoints
        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Registrar(RegistrarUserDTO credencial)
        {
            // Busco Persona mediante id y empiezo a dar de alta los datos de Usuario
            var persona = await reposPersona.GetById(credencial.IdPersona.Value);

            var usuario = new GOPUser 
            { 
                UserName = persona.Email, 
                Email = persona.Email,
                PersonaId = persona.Id,
            };

            var resultado = await userManager.CreateAsync(usuario, credencial.Contraseña);
            
            if (resultado.Succeeded)
            {
                //CREA CLAIM EMAIL Y ROL
                await userManager.AddClaimAsync(usuario, new Claim("Email", usuario.Email));
                await userManager.AddClaimAsync(usuario, new Claim(ClaimTypes.Role, credencial.Rol.ToString()));

                return await ConstruirToken(usuario.Email);
            }
            else return BadRequest(resultado.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Login(CredencialUserDTO credencial)
        {
            var resultado = await signInManager.PasswordSignInAsync(credencial.Email,
                                                                    credencial.Psw,
                                                                    isPersistent: false,
                                                                    lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencial.Email);
            }
            else
            {
                return BadRequest("Usuario o Pasword Incorrecto...");
            }
        }

        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Renovar()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "Email").FirstOrDefault();
            var email = emailClaim.Value;

            return await ConstruirToken(email);
        }

        [HttpPost("nuevacontraseña")]
        public async Task<bool> CambiarContraseña(CredencialUserDTO credencial)
        {
            var user = await userManager.FindByEmailAsync(credencial.Email);

            if (user != null)
            {
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, credencial.Psw);
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return true;
                else return false;
            }
            else return false;
        }

        //ROLES
        [HttpPost("CargarRol")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> CargarRol(RolDTO rol)
        {
            GOPUser usuario = await ObtenerUsuario(rol.Email);
            await userManager.AddClaimAsync(usuario, new Claim(ClaimTypes.Role, rol.Rol.ToString()));
            return NoContent();
        }

        [HttpPost("BorrarRol")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> BorrarRol(RolDTO rol)
        {
            GOPUser usuario = await ObtenerUsuario(rol.Email);
            IList<Claim> claims = await userManager.GetClaimsAsync(usuario);
            await userManager.RemoveClaimAsync(usuario, claims.Where(claim => claim.Type == ClaimTypes.Role).FirstOrDefault());
            return NoContent();
        }

        [HttpGet("UsersRoles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<PersonaRolesDTO>> GetUser()
        {
            List<PersonaRolesDTO> Listpersonas = new();
            var personas = await reposPersona.GetActivos();
            foreach (var p in personas)
            {
                PersonaRolesDTO persona = new();
                GOPUser usuario = await ObtenerUsuario(p.Email);
                persona.Id = p.Id;
                persona.DNI = p.DNI;
                persona.Nombre = p.Nombre;
                persona.Apellido = p.Apellido;
                persona.Email = p.Email;
                persona.Telefono = p.Telefono;
                if (usuario != null)
                {
                    IList<Claim> claims = await userManager.GetClaimsAsync(usuario);
                    persona.Rol = claims.Where(claim => claim.Type == ClaimTypes.Role).FirstOrDefault().Value;
                }
                else
                    persona.Rol = null;
                Listpersonas.Add(persona);
            }

            return Ok(Listpersonas);
        }

        #endregion

        #region Metodos
        private async Task<GOPUser> ObtenerUsuario(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        [HttpGet("GetRol/{email}")]
        public async Task<RolDTO> ObtenerRol(string email)
        {
            RolDTO rol = new();
            EnumRol rolEnum;

            var user = await userManager.FindByEmailAsync(email);
            IList<Claim> claims = await userManager.GetClaimsAsync(user);
            rol.Email = email;

            var tieneRol = claims.Where(claim => claim.Type == ClaimTypes.Role).FirstOrDefault();
            if (tieneRol != null)
            {
                Enum.TryParse(tieneRol.Value, out rolEnum);
                rol.Rol = rolEnum;
            }

            return rol;
        }

        [HttpGet("ExisteUsuario/{email}")]
        public async Task<bool> ExisteUsuario(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
                return true;
            else
                return false;
        }

        private async Task<RespuestaAutenticacionDTO> ConstruirToken(string email)
        {
            GOPUser usuario = await ObtenerUsuario(email);
            var claims = new List<Claim>()
            {
                new Claim("Id",usuario.Id)
            };
            var claimsDB = await userManager.GetClaimsAsync(usuario);
            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:llave"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddMinutes(minexpiracion);
            var securityToken = new JwtSecurityToken(issuer: null,
                                                     audience: null,
                                                     claims: claims,
                                                     expires: expiracion,
                                                     signingCredentials: creds);

            return new RespuestaAutenticacionDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expira = expiracion
            };
        }
        #endregion
    }
}             

