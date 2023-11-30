using GOP.Shared.DTOs.Entity;
using GOP.Shared.DTOs.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using RTools_NTS.Util;
using Newtonsoft.Json.Linq;
using GOP.Client.Pages.Auth;
using GOP.BD.Data;

namespace GOP.Client.Services
{
    public class UserService : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/user";
        private IJSRuntime _jsRuntime;

        private static readonly string TOKENKEY = "TOKENKEY";
        private static readonly string EXPIRATIONTOKENKEY = "EXPIRATIONTOKENKEY";
        private AuthenticationState Anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public UserService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKENKEY);

            if (string.IsNullOrEmpty(token))
            {
                return Anonimo;
            }

            var expiracion = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", EXPIRATIONTOKENKEY);
            DateTime tiempoExpiracion;

            if (!DateTime.TryParse(expiracion, out tiempoExpiracion))
            {
                if (TokenExpirado(tiempoExpiracion))
                {
                    await Limpiar();
                    return Anonimo;
                }

                if (DebeRenovarToken(tiempoExpiracion))
                {
                    token = await RenovarToken(token);
                }
            }

            return ConstruirAuthenticationState(token);
        }

        public async Task ManejarRenovacionToken()
        {
            var expiracion = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", EXPIRATIONTOKENKEY);
            DateTime tiempoExpiracion;

            if (DateTime.TryParse(expiracion, out tiempoExpiracion))
            {
                if (TokenExpirado(tiempoExpiracion))
                {
                    await Logout();
                }

                if (DebeRenovarToken(tiempoExpiracion))
                {
                    var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKENKEY);
                    var nuevoToken = await RenovarToken(token);
                    var authState = ConstruirAuthenticationState(nuevoToken);
                    NotifyAuthenticationStateChanged(Task.FromResult(authState));
                }
            }
        }

        private async Task<string> RenovarToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var nuevoTokenResponse = await _httpClient.GetFromJsonAsync<RespuestaAutenticacionDTO>($"{route}/RenovarToken");
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKENKEY, nuevoTokenResponse.Token);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", EXPIRATIONTOKENKEY, nuevoTokenResponse.Expira.ToString());
            return nuevoTokenResponse.Token;
        }

        private bool DebeRenovarToken(DateTime tiempoExpiracion)
        {
            return tiempoExpiracion.Subtract(DateTime.UtcNow) < TimeSpan.FromMinutes(60);
        }

        private bool TokenExpirado(DateTime tiempoExpiracion)
        {
            return tiempoExpiracion <= DateTime.UtcNow;
        }

        private async Task Limpiar()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TOKENKEY);
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", EXPIRATIONTOKENKEY);
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        private AuthenticationState ConstruirAuthenticationState(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task<IEnumerable<PersonaRolesDTO>> GetPersonasRoles()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<PersonaRolesDTO>>($"{route}/UsersRoles");
        }

        public async Task<RolDTO> GetRol(string email)
        {
            return await _httpClient.GetFromJsonAsync<RolDTO>($"{route}/GetRol/{email}");
        }

        public async Task<HttpResponseMessage> CargarRol(RolDTO rol)
        {
            string objSerealizado = Newtonsoft.Json.JsonConvert.SerializeObject(rol);
            var respuesta = await _httpClient.PostAsync($"{route}/CargarRol",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));

            return respuesta;
        }

        public async Task<HttpResponseMessage> BorrarRol(RolDTO rol)
        {
            string objSerealizado = Newtonsoft.Json.JsonConvert.SerializeObject(rol);
            var respuesta = await _httpClient.PostAsync($"{route}/BorrarRol",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));

            return respuesta;
        }

        public async Task<HttpResponseMessage> Registrar(RegistrarUserDTO obj)
        {
            string objSerealizado = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}/registrar",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));

            return respuesta;
        }

        public async Task<HttpResponseMessage> Login(CredencialUserDTO obj)
        {
            string objSerealizado = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}/login",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));

            if (respuesta.IsSuccessStatusCode)
            {
                var rta = await respuesta.Content.ReadFromJsonAsync<RespuestaAutenticacionDTO>();
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKENKEY, rta.Token);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", EXPIRATIONTOKENKEY, rta.Expira.ToString());
                var authState = ConstruirAuthenticationState(rta.Token);
                NotifyAuthenticationStateChanged(Task.FromResult(authState));
            }

            return respuesta;
        }

        public async Task Logout()
        {
            await Limpiar();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }

        public async Task<bool> ExisteUsuario(string email)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"{route}/ExisteUsuario/{email}");
        }

        public async Task<bool> CambioPsw(CredencialUserDTO obj)
        {
            string objSerealizado = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}/nuevacontraseña",
                   new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));

            return respuesta.IsSuccessStatusCode;
        }
    }
}
