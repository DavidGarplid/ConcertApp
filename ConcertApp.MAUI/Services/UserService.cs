﻿using ConcertApp.Data.DTO;
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
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:5001/api/user"; // Adjust URL if necessary

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoginAsync(User user)
        {
            try
            {
                string json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}/login", content);
                string responseContent = await response.Content.ReadAsStringAsync();

                Debug.WriteLine($"Response Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true 
                    };

                    var userDto = JsonSerializer.Deserialize<UserDto>(responseContent, options);
                    
                 
                    Preferences.Set("UserName", userDto.Name);   
                    Preferences.Set("UserEmail", userDto.Email); 
                    Preferences.Set("UserID", (int)userDto.ID);

                    

                    return "Login successful";
                }
                else
                {
                    return "Login failed: " + responseContent;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
                return "Login failed: Unexpected error";
            }
        }


    }
}
