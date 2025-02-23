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
        private readonly string _baseUrl = "https://localhost:5001/api/booking"; 

        public BookingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            var apiUrl = $"{_baseUrl}/delete/{bookingId}"; 
            var response = await _httpClient.DeleteAsync(apiUrl);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Booking>> GetBookingsByUserIdAsync(int userID) 
        {
            Debug.WriteLine($"UserId: {userID}");
            var response = await _httpClient.GetAsync($"{_baseUrl}/user/{userID}");  
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch bookings");
            }

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Booking>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
