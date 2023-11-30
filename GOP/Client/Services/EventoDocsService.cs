using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class EventoDocsService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/EventoDoc";

        public EventoDocsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<EventoDocDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getEventoDoc");
            return JsonConvert.DeserializeObject<IEnumerable<EventoDocDTO>>(await respuesta);
        }


        public async Task<EventoDocDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<EventoDocDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(EventoDocDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(EventoDocDTO obj)
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
