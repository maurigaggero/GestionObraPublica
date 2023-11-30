using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class EstructuraService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/EstructuraTipo";

        public EstructuraService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<EstructuraTipoDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getfull");
            return JsonConvert.DeserializeObject<IEnumerable<EstructuraTipoDTO>>(await respuesta);
        }


        public async Task<EstructuraTipoDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<EstructuraTipoDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(EstructuraTipoDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(EstructuraTipoDTO obj)
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
