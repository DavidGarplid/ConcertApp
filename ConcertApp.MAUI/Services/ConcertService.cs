using ConcertApp.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.Services
{
    public class ConcertService : IConcertService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:5001/api/concert";

        public ConcertService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Concert>> GetAllConcertsAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            if (!response.IsSuccessStatusCode) return new List<Concert>();

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Concert>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
