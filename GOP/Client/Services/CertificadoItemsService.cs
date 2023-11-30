using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class CertificadoItemsService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/Certificadoitem";

        public CertificadoItemsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CertificadoItemDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<CertificadoItemDTO>($"{route}/{id}");
        }

        public async Task<IEnumerable<CertificadoItemDTO>> GetFull(int id)
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getCertificadoItem/{id}");
            return JsonConvert.DeserializeObject<IEnumerable<CertificadoItemDTO>>(await respuesta);
        }

        public async Task<HttpResponseMessage> GenerarExcelItemsPorEstructura(int id)
        {
            return await _httpClient.GetAsync($"{route}/Reportes/Excel/Estructura/{id}");
        }

        public async Task<HttpResponseMessage> GenerarExcelItemsPorFrente(int id)
        {
            return await _httpClient.GetAsync($"{route}/Reportes/Excel/Frente/{id}");
        }

        public async Task<HttpResponseMessage> Post(CertificadoItemDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(CertificadoItemDTO obj)
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
