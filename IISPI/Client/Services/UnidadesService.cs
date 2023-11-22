using IISPI.BD.Data.Entity;
using IISPI.Shared.DTOs.IINSPIDtos;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace IISPI.Client.Services
{
    public class UnidadesService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "iispi/api/unidades";

        public UnidadesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Unidad>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getfull");
            return JsonConvert.DeserializeObject<IEnumerable<Unidad>>(await respuesta);
        }

        public async Task<IEnumerable<Unidad>> GetActivos()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getactivos");
            return JsonConvert.DeserializeObject<IEnumerable<Unidad>>(await respuesta);
        }


        public async Task<Unidad> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<Unidad>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(UnidadesDTO unidad)
        {
            string objSerealizado = JsonConvert.SerializeObject(unidad);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(UnidadesDTO unidad)
        {
            string objSerealizado = JsonConvert.SerializeObject(unidad);
            var respuesta = await _httpClient.PutAsync($"{route}/{unidad.CodUnidad}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Baja(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"{route}/baja/{id}");
            return respuesta;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var respuesta = await _httpClient.DeleteAsync($"{route}/{id}");
            return respuesta;
        }
    }
}