using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class UnidadesService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/unidades";

        public UnidadesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<UnidadDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getfull");
            return JsonConvert.DeserializeObject<IEnumerable<UnidadDTO>>(await respuesta);
        }


        public async Task<UnidadDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<UnidadDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(UnidadDTO unidad)
        {
            string objSerealizado = JsonConvert.SerializeObject(unidad);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(UnidadDTO unidad)
        {
            string objSerealizado = JsonConvert.SerializeObject(unidad);
            var respuesta = await _httpClient.PutAsync($"{route}/{unidad.Id}",
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