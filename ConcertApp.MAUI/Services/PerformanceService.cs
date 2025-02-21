using ConcertApp.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:5001/api/performances";

        public PerformanceService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Performance>> GetPerformancesByConcertIdAsync(int concertId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}?concertId={concertId}");
            if (!response.IsSuccessStatusCode) return new List<Performance>();

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Performance>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }


    }
}
