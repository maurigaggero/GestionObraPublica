using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class CertificadoItemControlDocsService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/CertificadoItemControlDocs";

        public CertificadoItemControlDocsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CertificadoItemControlDocDTO>> GetFull(int itemControlId)
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getCertificadoItemControlDoc/{itemControlId}");
            return JsonConvert.DeserializeObject<IEnumerable<CertificadoItemControlDocDTO>>(await respuesta);
        }


        public async Task<CertificadoItemControlDocDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<CertificadoItemControlDocDTO>($"{route}/{id}");
        }


        public async Task<HttpResponseMessage> Post(CertificadoItemControlDocDTO item)
        {
            string objSerealizado = JsonConvert.SerializeObject(item);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(CertificadoItemControlDocDTO item)
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
