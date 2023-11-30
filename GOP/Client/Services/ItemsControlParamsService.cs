using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ItemsControlParamsService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/itemscontrolsparams";

        public ItemsControlParamsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ItemControlParamDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getitemcontrolparams");
            return JsonConvert.DeserializeObject<IEnumerable<ItemControlParamDTO>>(await respuesta);
        }


        public async Task<ItemControlParamDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ItemControlParamDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(ItemControlParamDTO item)
        {
            string objSerealizado = JsonConvert.SerializeObject(item);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(ItemControlParamDTO item)
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
