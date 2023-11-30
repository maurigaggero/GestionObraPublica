using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ContratoItemService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/contratoitem";

        public ContratoItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ContratoItemDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ContratoItemDTO>($"{route}/{id}");
        }

        public async Task<IEnumerable<ContratoItemDTO>> GetFull(int id)
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getByContratoId/{id}");
            return JsonConvert.DeserializeObject<IEnumerable<ContratoItemDTO>>(await respuesta);
        }

        public async Task<HttpResponseMessage> Post(ContratoItemDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(ContratoItemDTO obj)
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