using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ContratoItemControlesParamsService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/ContratoItemControlParams";

        public ContratoItemControlesParamsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ContratoItemControlParamDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getContratoItemControlParam");
            return JsonConvert.DeserializeObject<IEnumerable<ContratoItemControlParamDTO>>(await respuesta);
        }


        public async Task<ContratoItemControlParamDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ContratoItemControlParamDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(ContratoItemControlParamDTO item)
        {
            string objSerealizado = JsonConvert.SerializeObject(item);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(ContratoItemControlParamDTO item)
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
