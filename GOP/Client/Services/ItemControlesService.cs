using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class ItemControlesService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/itemscontrols";

        public ItemControlesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ItemControlDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getitemcontrols");
            return JsonConvert.DeserializeObject<IEnumerable<ItemControlDTO>>(await respuesta);
        }

        public async Task<IEnumerable<ItemControlDTO>> GetByItem(int id)
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/get/{id}");
            return JsonConvert.DeserializeObject<IEnumerable<ItemControlDTO>>(await respuesta);
        }

        public async Task<ItemControlDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ItemControlDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(ItemControlDTO item)
        {
            string objSerealizado = JsonConvert.SerializeObject(item);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(ItemControlDTO item)
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
