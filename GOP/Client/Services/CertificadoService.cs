using GOP.BD.Data.Entity;
using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class CertificadoService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/Certificado";

        public CertificadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CertificadoDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getCertificado");
            return JsonConvert.DeserializeObject<IEnumerable<CertificadoDTO>>(await respuesta);
        }

        public async Task<IEnumerable<CertificadoDTO>> GetFull(int idContrato)
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getCertificadoPorContrato/{idContrato}");
            return JsonConvert.DeserializeObject<IEnumerable<CertificadoDTO>>(await respuesta);
        }


        public async Task<CertificadoDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<CertificadoDTO>($"{route}/{id}");
        }

        public async Task<CertificadoDTO> GetByIdNoFul(int id)
        {
            return await _httpClient.GetFromJsonAsync<CertificadoDTO>($"{route}/GetByIdNoFul/{id}");
        }

        public async Task<HttpResponseMessage> Post(CertificadoDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Definitivo(int id)
        {
            return await _httpClient.PostAsync($"{route}/def/{id}", null);
        }

        public async Task<HttpResponseMessage> GenerarExcel(int id)
        {
            return await _httpClient.GetAsync($"{route}/Reportes/Excel/{id}");
        }

        public async Task<HttpResponseMessage> Put(CertificadoDTO obj)
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
