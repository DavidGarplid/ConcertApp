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

        public async Task<bool> IsPerformanceBookedAsync(int performanceId)
        {
            try
            {
                int userId = Preferences.Get("UserID", 0);
                if (userId == 0) return false;

                var response = await _httpClient.GetAsync($"{_baseUrl2}/isBooked?performanceId={performanceId}&userId={userId}");
                if (!response.IsSuccessStatusCode) return false;

                string content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Dictionary<string, bool>>(content);
                return result != null && result.ContainsKey("IsBooked") && result["IsBooked"];
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

        public async Task<bool> DeleteBookingAsync(int performanceId)
        {
            try
            {
                int userId = Preferences.Get("UserID", 0);
                if (userId == 0)
                {
                    throw new Exception("User ID not found. Please log in again.");
                }

                var response = await _httpClient.DeleteAsync($"{_baseUrl2}/delete?performanceId={performanceId}&userId={userId}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting booking: {ex.Message}");
                return false;
            }
        }



    }
}
