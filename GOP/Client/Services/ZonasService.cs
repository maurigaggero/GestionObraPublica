using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ZonasService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/zonas";

        public ZonasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ZonaDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getfull");
            return JsonConvert.DeserializeObject<IEnumerable<ZonaDTO>>(await respuesta);
        }


        public async Task<ZonaDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ZonaDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(ZonaDTO zona)
        {
            string objSerealizado = JsonConvert.SerializeObject(zona);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(ZonaDTO zona)
        {
            string objSerealizado = JsonConvert.SerializeObject(zona);
            var respuesta = await _httpClient.PutAsync($"{route}/{zona.Id}",
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
