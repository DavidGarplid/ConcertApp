using ConcertApp.Data.DTO;
using ConcertApp.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:5001/api/booking"; // Adjust the URL

        public BookingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            var apiUrl = $"{_baseUrl}/delete/{bookingId}"; // Adjust the URL to match the controller route
            var response = await _httpClient.DeleteAsync(apiUrl);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Booking>> GetBookingsByUserIdAsync(int userID) //behöver denna fixas med Dto? osäker
        {
            Debug.WriteLine($"UserId: {userID}");
            var response = await _httpClient.GetAsync($"{_baseUrl}/user/{userID}");  // Endpoint for user-specific bookings
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch bookings");
            }

            var content = await response.Content.ReadAsStringAsync();
            
            Debug.WriteLine($"API response content: {content}");

            return JsonSerializer.Deserialize<List<Booking>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
