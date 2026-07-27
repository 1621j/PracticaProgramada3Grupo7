using System.Net.Http.Json;
using System.Text.Json;

namespace PracticaProgramada3Grupo7.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<T>> ObtenerListaAsync<T>(
            string endpoint)
        {
            HttpResponseMessage respuesta =
                await _httpClient.GetAsync(endpoint);

            if (!respuesta.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    await ObtenerMensajeErrorAsync(respuesta));
            }

            List<T>? elementos =
                await respuesta.Content
                    .ReadFromJsonAsync<List<T>>();

            return elementos ?? new List<T>();
        }

        public async Task<T?> ObtenerAsync<T>(
            string endpoint)
        {
            HttpResponseMessage respuesta =
                await _httpClient.GetAsync(endpoint);

            if (respuesta.StatusCode ==
                System.Net.HttpStatusCode.NotFound)
            {
                return default;
            }

            if (!respuesta.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    await ObtenerMensajeErrorAsync(respuesta));
            }

            return await respuesta.Content
                .ReadFromJsonAsync<T>();
        }

        public async Task<HttpResponseMessage> CrearAsync<T>(
            string endpoint,
            T objeto)
        {
            return await _httpClient.PostAsJsonAsync(
                endpoint,
                objeto);
        }

        public async Task<HttpResponseMessage> ActualizarAsync<T>(
            string endpoint,
            T objeto)
        {
            return await _httpClient.PutAsJsonAsync(
                endpoint,
                objeto);
        }

        public async Task<HttpResponseMessage> EliminarAsync(
            string endpoint)
        {
            return await _httpClient.DeleteAsync(endpoint);
        }

        public async Task<string> ObtenerMensajeErrorAsync(
            HttpResponseMessage respuesta)
        {
            string contenido =
                await respuesta.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(contenido))
            {
                return "No fue posible completar la operación.";
            }

            try
            {
                using JsonDocument documento =
                    JsonDocument.Parse(contenido);

                JsonElement raiz = documento.RootElement;

                if (raiz.TryGetProperty(
                    "mensaje",
                    out JsonElement mensaje))
                {
                    return mensaje.GetString()
                        ?? "No fue posible completar la operación.";
                }

                if (raiz.TryGetProperty(
                    "Mensaje",
                    out JsonElement mensajeMayuscula))
                {
                    return mensajeMayuscula.GetString()
                        ?? "No fue posible completar la operación.";
                }
            }
            catch (JsonException)
            {
                return contenido;
            }

            return contenido;
        }
    }
}