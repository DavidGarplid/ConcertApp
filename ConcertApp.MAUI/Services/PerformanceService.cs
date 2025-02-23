using ConcertApp.Data.DTO;
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
        private readonly string _baseUrl = "https://localhost:5001/api/performance";
        private readonly string _baseUrl2 = "https://localhost:5001/api/booking";

        public PerformanceService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Performance>> GetPerformancesByConcertIdAsync(int concertId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/byconcert/{concertId}");
            if (!response.IsSuccessStatusCode) return new List<Performance>();

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Performance>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> IsPerformanceBookedAsync(int performanceId, int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl2}/isBooked?performanceId={performanceId}&userId={userId}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Error: Response not successful.");
                    return false;
                }

                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response content: {content}");  // Log the raw response content for debugging
                var result = JsonSerializer.Deserialize<Dictionary<string, bool>>(content);

                // Log the deserialized dictionary to confirm correct values
                Console.WriteLine($"Deserialized result: {result}");

                // Check if 'IsBooked' key exists and return its value
                return result != null && result.ContainsKey("isBooked") && result["isBooked"];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking booking status: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CreateBookingAsync(int performanceId)
        {
            try
            {
                int userId = Preferences.Get("UserID", 0);
                string userName = Preferences.Get("UserName", string.Empty);
                string userEmail = Preferences.Get("UserEmail", string.Empty);

                if (userId == 0 || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userEmail))
                {
                    throw new Exception("User data is missing. Please log in again.");
                }

                var bookingDto = new BookingDto
                {
                    UserID = userId,
                    PerformanceID = performanceId,
                    Name = userName,
                    Email = userEmail
                };

                var json = JsonSerializer.Serialize(bookingDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl2}/create", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating booking: {ex.Message}");
                return false;
            }
        }

        



    }
}
