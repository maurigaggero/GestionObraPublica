using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ContratoDocsService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/ContratoDoc";

        public ContratoDocsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ContratoDocDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getContratoDoc");
            return JsonConvert.DeserializeObject<IEnumerable<ContratoDocDTO>>(await respuesta);
        }

        public async Task<IEnumerable<ContratoDocDTO>> GetFull(int id)
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getByContratoId/{id}");
            return JsonConvert.DeserializeObject<IEnumerable<ContratoDocDTO>>(await respuesta);
        }


        public async Task<ContratoDocDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ContratoDocDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(ContratoDocDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(ContratoDocDTO obj)
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
