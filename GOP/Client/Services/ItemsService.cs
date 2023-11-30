using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ItemsService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/items";

        public ItemsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ItemDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getitems");
            return JsonConvert.DeserializeObject<IEnumerable<ItemDTO>>(await respuesta);
        }


        public async Task<ItemDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ItemDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(ItemDTO item)
        {
            string objSerealizado = JsonConvert.SerializeObject(item);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(ItemDTO item)
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