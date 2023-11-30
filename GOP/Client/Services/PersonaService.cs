using GOP.Shared.DTOs.Entity;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class PersonaService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/personas";

        public PersonaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<PersonaDTO>> GetFull()
        {
            var respuesta = _httpClient.GetStringAsync($"{route}/getfull");
            return JsonConvert.DeserializeObject<IEnumerable<PersonaDTO>>(await respuesta);
        }


        public async Task<PersonaDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<PersonaDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(PersonaDTO persona)
        {
            string objSerealizado = JsonConvert.SerializeObject(persona);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(PersonaDTO persona)
        {
            string objSerealizado = JsonConvert.SerializeObject(persona);
            var respuesta = await _httpClient.PutAsync($"{route}/{persona.Id}",
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
