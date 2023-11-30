using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ContratoItemControlesDocsService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/ContratoItemControlDocs";

        public ContratoItemControlesDocsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ContratoItemControlDocDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getContratoItemControlDoc");
            return JsonConvert.DeserializeObject<IEnumerable<ContratoItemControlDocDTO>>(await respuesta);
        }


        public async Task<ContratoItemControlDocDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ContratoItemControlDocDTO>($"{route}/{id}");
        }


        public async Task<HttpResponseMessage> Post(ContratoItemControlDocDTO item)
        {
            string objSerealizado = JsonConvert.SerializeObject(item);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(ContratoItemControlDocDTO item)
        {
            string objSerealizado = JsonConvert.SerializeObject(item);
            var respuesta = await _httpClient.PutAsync($"{route}/{item.Id}",
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