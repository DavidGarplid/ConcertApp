using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using AutoMapper;
using System.Threading.Tasks;
using ConcertApp.MAUI.Models;

namespace ConcertApp.MAUI.Services
{
    public class RestService<T> : IRestService<T>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IMapper _mapper;
        private readonly string _endpoint;

        public RestService(HttpClient client, IMapper mapper, string endpoint)
        {
            _client = client;
            _mapper = mapper;
            _endpoint = endpoint;

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<T>?> RefreshDataAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(_endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<T>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
            return null;
        }

        public async Task SaveItemAsync(T item, bool isNewItem)
        {
            try
            {
                string json = JsonSerializer.Serialize(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = isNewItem
                    ? await _client.PostAsync(_endpoint, content)
                    : await _client.PutAsync(_endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Item successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public async Task DeleteItemAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{_endpoint}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Item successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
