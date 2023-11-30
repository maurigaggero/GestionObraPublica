using GOP.Shared.DTOs;
using GOP.Shared.DTOs.Entity;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace GOP.Client.Services
{
    public class EventoService
    {
        private readonly HttpClient _httpClient;
        private readonly string route = "GOP/api/Evento";

        public EventoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<EventoDTO>> GetFull(FiltroEventoDTO filtro)
        {
            var queryParams = new Dictionary<string, string>();

            if (filtro.TipoId.HasValue)
            {
                queryParams.Add("TipoId", filtro.TipoId.ToString());
            }
            queryParams.Add("FechaDesde", filtro.FechaDesde.ToString());
            queryParams.Add("FechaHasta", filtro.FechaHasta.ToString());
            if (filtro.ContratoId.HasValue)
            {
                queryParams.Add("ContratoId", filtro.ContratoId.ToString());
            }
            queryParams.Add("Pagina", filtro.Pagina.ToString());
            queryParams.Add("RegistrosPorPagina", filtro.RegistrosPorPagina.ToString());

            var queryString = QueryHelpers.AddQueryString($"{route}/getEvento/", queryParams);

            var respuesta = _httpClient.GetStringAsync(queryString);
            return JsonConvert.DeserializeObject<IEnumerable<EventoDTO>>(await respuesta);
        }

        public async Task<IEnumerable<EventoDTO>> GetReporte(FiltroEventoReporteDTO filtro)
        {
            var queryParams = new Dictionary<string, string>();

            if (filtro.TipoId.HasValue)
            {
                queryParams.Add("TipoId", filtro.TipoId.ToString());
            }
            queryParams.Add("FechaDesde", filtro.FechaDesde.ToString());
            queryParams.Add("FechaHasta", filtro.FechaHasta.ToString());
            if (filtro.ContratoId.HasValue)
            {
                queryParams.Add("ContratoId", filtro.ContratoId.ToString());
            }
            if (filtro.ZonaId.HasValue)
            {
                queryParams.Add("ZonaId", filtro.ZonaId.ToString());
            }
            if (filtro.FrenteId.HasValue)
            {
                queryParams.Add("FrenteId", filtro.FrenteId.ToString());
            }

            var queryString = QueryHelpers.AddQueryString($"{route}/reportes/", queryParams);

            var respuesta = _httpClient.GetStringAsync(queryString);
            var res = JsonConvert.DeserializeObject<IEnumerable<EventoDTO>>(await respuesta);
            return res;
        }

        public async Task<EventoDTO> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<EventoDTO>($"{route}/{id}");
        }

        public async Task<HttpResponseMessage> Post(EventoDTO obj)
        {
            string objSerealizado = JsonConvert.SerializeObject(obj);
            var respuesta = await _httpClient.PostAsync($"{route}",
                        new StringContent(objSerealizado, UnicodeEncoding.UTF8, "application/json"));
            return respuesta;
        }

        public async Task<HttpResponseMessage> Put(EventoDTO obj)
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
