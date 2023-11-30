using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ItemControlDocsService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/itemscontrolsdocs";

        public ItemControlDocsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ItemControlDocDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getitemscontroldocs");
            return JsonConvert.DeserializeObject<IEnumerable<ItemControlDocDTO>>(await respuesta);
        }


        public async Task<ItemControlDocDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ItemControlDocDTO>($"{route}/{id}");
        }


        public async Task<HttpResponseMessage> Post(ItemControlDocDTO item)
        {
            string objSerealizado = JsonConvert.SerializeObject(item);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(ItemControlDocDTO item)
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
