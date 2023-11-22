using IISPI.BD.Data;
using IISPI.BD.Data.Entity;
using IISPI.Repositorio.Repos;
using IISPI.Server.Controllers.IISPIControllers;
using IISPI.Shared.DTOs.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IISPI.Server.Controllers.Identity
{
    [ApiController]
    [Route("iispi/api/user")]
    public class IISPIUsersController : ControllerBase
    {
        const double minexpiracion = 10;
        private readonly UserManager<IISPIUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IISPIUser> signInManager;
        private readonly IPersonasRepositorio reposPersona;

        public IISPIUsersController(UserManager<IISPIUser> userManager,
                                    IConfiguration configuration,
                                    SignInManager<IISPIUser> signInManager,
                                    IPersonasRepositorio reposPersona)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.reposPersona = reposPersona;
        }

        #region EndPoints
        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Registrar(RegistrarUserDTO credencial)
        {
            var usuario = new IISPIUser { UserName = credencial.Email, Email = credencial.Email };
            var resultado = await userManager.CreateAsync(usuario, credencial.Psw);
            //Falta controlador para grabar persona y relacionar ID al User
            if (resultado.Succeeded)
            {
                //ACTUALIZA CLAIM EMAIL
                await userManager.AddClaimAsync(usuario, new Claim("Email", usuario.Email));
                //AGREGA PERSONA
                try
                {
                    if (credencial == null) return BadRequest("Datos incorrectos");
                    Persona persona = PersonasController.MapeaPersona(credencial);

                    int personaId = await reposPersona.Insert(persona);
                    if (personaId > 0)
                    {
                        usuario.PersonaId = personaId;
                        resultado = await userManager.UpdateAsync(usuario);
                        if (resultado.Succeeded)
                        {
                        }
                        else return BadRequest("No se pudo agregar la persona");
                    }
                    else return BadRequest("No se pudo agregar la persona");
                }
                catch (Exception e)
                {
                    return BadRequest("No se pudo agregar la persona.\r\n" + e.Message);
                }
                return await ConstruirToken(credencial.Email);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
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

        //ROLES
        //"Admin"
        //"Central" 
        //"Obra"
        //"Consulta"
        //"Autoridad"
        [HttpPost("CargarRol")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> CargarRol(RolDTO rol)
        {
            IISPIUser usuario = await ObtenerUsuario(rol.Email);
            await userManager.AddClaimAsync(usuario, new Claim(rol.Rol.ToString(), ""));
            return NoContent();
        }

        [HttpPost("BorrarRol")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> BorrarRol(RolDTO rol)
        {
            IISPIUser usuario = await ObtenerUsuario(rol.Email);
            await userManager.RemoveClaimAsync(usuario, new Claim(rol.Rol.ToString(), ""));
            return NoContent();
        }
        #endregion

        #region Metodos
        private async Task<IISPIUser> ObtenerUsuario(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        private async Task<RespuestaAutenticacionDTO> ConstruirToken(string email)
        {
            IISPIUser usuario = await ObtenerUsuario(email);
            var claims = new List<Claim>()
            {
                new Claim("Email", usuario.Email),
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

