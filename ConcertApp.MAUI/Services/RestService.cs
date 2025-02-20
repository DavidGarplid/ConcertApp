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
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;


        public RestService(HttpClient httpClient, string entity)
        {
            _httpClient = httpClient;
            _baseUrl = $"https://localhost:5001/api/{entity}";
        }
        public async Task<List<T>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            if (!response.IsSuccessStatusCode) return null;

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<T>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return default;

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public async Task<bool> CreateAsync(T entity)
        {
            string json = JsonSerializer.Serialize(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateAsync(int id, T entity)
        {
            string json = JsonSerializer.Serialize(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
