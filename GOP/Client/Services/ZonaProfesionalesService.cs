using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ZonaProfesionalesService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/zonasprofesionales";

        public ZonaProfesionalesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ZonaProfesionalDTO>> GetFull(int zonaId)
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getZonaProfesionals/{zonaId}");
            return JsonConvert.DeserializeObject<IEnumerable<ZonaProfesionalDTO>>(await respuesta);
        }


        public async Task<ZonaProfesionalDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ZonaProfesionalDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(ZonaProfesionalDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(ZonaProfesionalDTO obj)
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
