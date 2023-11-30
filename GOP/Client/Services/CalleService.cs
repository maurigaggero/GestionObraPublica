using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class CalleService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/Calle";

        public CalleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CalleDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getfull");
            return JsonConvert.DeserializeObject<IEnumerable<CalleDTO>>(await respuesta);
        }


        public async Task<CalleDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<CalleDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(CalleDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(CalleDTO obj)
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
