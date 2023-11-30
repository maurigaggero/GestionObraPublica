using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class EmpresasService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/empresas";

        public EmpresasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<EmpresaDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getfull");
            return JsonConvert.DeserializeObject<IEnumerable<EmpresaDTO>>(await respuesta);
        }


        public async Task<EmpresaDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmpresaDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(EmpresaDTO empresa)
        {
            string objSerealizado = JsonConvert.SerializeObject(empresa);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(EmpresaDTO empresa)
        {
            string objSerealizado = JsonConvert.SerializeObject(empresa);
            var respuesta = await _httpClient.PutAsync($"{route}/{empresa.Id}",
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