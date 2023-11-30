using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class CertificadoItemControlsService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/Certificadoitemcontrol";

        public CertificadoItemControlsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CertificadoItemControlDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getCertificadoItemControl");
            return JsonConvert.DeserializeObject<IEnumerable<CertificadoItemControlDTO>>(await respuesta);
        }

        public async Task<CertificadoItemControlDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<CertificadoItemControlDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(CertificadoItemControlDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(CertificadoItemControlDTO obj)
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
