using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class CertificadoItemDefService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/CertificadoItemDef";

        public CertificadoItemDefService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CertificadoItemDefDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getfull");
            return JsonConvert.DeserializeObject<IEnumerable<CertificadoItemDefDTO>>(await respuesta);
        }

        public async Task<CertificadoItemDefDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<CertificadoItemDefDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> GenerarExcel(int id)
        {
            return await _httpClient.GetAsync($"{route}/Reportes/Excel/{id}");
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
