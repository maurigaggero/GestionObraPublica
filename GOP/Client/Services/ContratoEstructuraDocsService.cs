using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ContratoEstructuraDocsService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/ContratoEstructuraDoc";

        public ContratoEstructuraDocsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ContratoEstructuraDocDTO>> GetFull(int id)
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getContratoEstructuraDoc/{id}");
            return JsonConvert.DeserializeObject<IEnumerable<ContratoEstructuraDocDTO>>(await respuesta);
        }


        public async Task<ContratoEstructuraDocDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ContratoEstructuraDocDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(ContratoEstructuraDocDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(ContratoEstructuraDocDTO obj)
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
