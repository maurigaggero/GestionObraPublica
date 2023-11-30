using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class EmpresaProfesionalesService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/empprofesionales";

        public EmpresaProfesionalesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<EmpresaProfesionalDTO>> GetFull(int empresaId)
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getempresaprofesionals/{empresaId}");
            return JsonConvert.DeserializeObject<IEnumerable<EmpresaProfesionalDTO>>(await respuesta);
        }


        public async Task<EmpresaProfesionalDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmpresaProfesionalDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(EmpresaProfesionalDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(EmpresaProfesionalDTO obj)
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
