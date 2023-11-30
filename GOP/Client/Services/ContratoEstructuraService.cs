using GOP.Shared.DTOs;
using GOP.Shared.DTOs.Entity;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ContratoEstructuraService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/ContratoEstructura";

        public ContratoEstructuraService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ContratoEstructuraDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getfull/");
            return JsonConvert.DeserializeObject<IEnumerable<ContratoEstructuraDTO>>(await respuesta);
        }

        public async Task<IEnumerable<ContratoEstructuraDTO>> GetFull(int contratoId)
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getContratoEstructura/{contratoId}");
            return JsonConvert.DeserializeObject<IEnumerable<ContratoEstructuraDTO>>(await respuesta);
        }

        public async Task<IEnumerable<ContratoEstructuraDTO>> GetFull(PaginacionDTO paginacion, int id)
        {
            var queryParams = new Dictionary<string, string>
            {
                ["Pagina"] = paginacion.Pagina.ToString(),
                ["RegistrosPorPagina"] = paginacion.RegistrosPorPagina.ToString()
            };

            var queryString = QueryHelpers.AddQueryString($"{route}/getByContratoId/{id}", queryParams);

            var respuesta = _httpClient.GetStringAsync(queryString);
            return JsonConvert.DeserializeObject<IEnumerable<ContratoEstructuraDTO>>(await respuesta);
        }

        public async Task<ContratoEstructuraDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ContratoEstructuraDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(ContratoEstructuraDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(ContratoEstructuraDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PutAsync($"{route}/{obj.Id}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Baja(int id)
        {
            var respuesta = await _httpClient.PutAsync($"{route}/baja/{id}", null);
            return respuesta;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"{route}/{id}");
            return respuesta;
        }
    }
}
