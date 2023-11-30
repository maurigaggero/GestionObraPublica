using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ContratoService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/contrato";

        public ContratoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ContratoDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getContrato");
            return JsonConvert.DeserializeObject<IEnumerable<ContratoDTO>>(await respuesta);
        }

        public async Task<IEnumerable<ContratoDTO>> GetFullSimple()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getfull");
            var res = JsonConvert.DeserializeObject<IEnumerable<ContratoDTO>>(await respuesta);
            return res;
        }

        public async Task<ContratoDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ContratoDTO>($"{route}/{id}");
        }

        public async Task<ContratoDTO> GetNoFull(int id) //sergio
        {
            return await _httpClient.GetFromJsonAsync<ContratoDTO>($"{route}/getNoFull/{id}");
        }

        public async Task<string> GetCaratula(int id)  //sergio
        {
            var res = await _httpClient.GetStringAsync($"{route}/GetCaratula/{id}");
            return res;
        }

        public async Task<HttpResponseMessage> Post(ContratoDTO contrato)
        {
            string objSerealizado = JsonConvert.SerializeObject(contrato);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> GenerarExcel(int id)
        {
            return await _httpClient.GetAsync($"{route}/Reportes/Excel/{id}");
        }

        public async Task<HttpResponseMessage> GenerarExcelPorPeriodo(int id)
        {
            return await _httpClient.GetAsync($"{route}/Reportes/Excel/Periodos/{id}");
        }

        public async Task<HttpResponseMessage> GenerarExcelItemsPorZona()
        {
            return await _httpClient.GetAsync($"{route}/Reportes/Excel/ItemsZonas");
        }
        

        public async Task<HttpResponseMessage> Put(ContratoDTO contrato)
        {
            string objSerealizado = JsonConvert.SerializeObject(contrato);
            var respuesta = await _httpClient.PutAsync($"{route}/{contrato.Id}",
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
