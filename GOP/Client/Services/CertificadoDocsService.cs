using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class CertificadoDocsService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/CertificadoDoc";

        public CertificadoDocsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CertificadoDocDTO>> GetFull(int id)
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getfull/{id}");
            return JsonConvert.DeserializeObject<IEnumerable<CertificadoDocDTO>>(await respuesta);
        }

        public async Task<CertificadoDocDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<CertificadoDocDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(CertificadoDocDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(CertificadoDocDTO obj)
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
