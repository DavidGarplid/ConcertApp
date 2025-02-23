using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertApp.MAUI.Models;
using ConcertApp.MAUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.ViewModels
{
    //[ObservableObject]
    [QueryProperty(nameof(ConcertId), "concertId")]
    public partial class PerformanceViewModel : ObservableObject
    {
        private readonly IPerformanceService _performanceService;
        public ObservableCollection<Performance> Performances { get; set; }

        [ObservableProperty]
        private int concertId;

        public PerformanceViewModel(IPerformanceService performanceService)
        {
            _performanceService = performanceService;
            Performances = new ObservableCollection<Performance>();


        }

        partial void OnConcertIdChanged(int value)
        {
            LoadPerformances();
        }


        private async void LoadPerformances()
        {
            if (ConcertId == 0) return;

            var performances = await _performanceService.GetPerformancesByConcertIdAsync(ConcertId);
            int userId = Preferences.Get("UserID", 0);  // Get the logged-in user ID
            foreach (var performance in performances)
            {
                
                Performances.Add(performance);
            }
        }



        [RelayCommand]
        public async Task ToggleBooking(int performanceId)
        {
            var performance = Performances.FirstOrDefault(p => p.ID == performanceId);
            if (performance == null)
            {
                Debug.WriteLine("Performance not found.");
                return;
            }

            Debug.WriteLine($"Toggling booking for Performance ID: {performanceId}");

            // Get the logged-in user ID
            int userId = Preferences.Get("UserID", 0);

            // Check if already booked
            var isBooked = await _performanceService.IsPerformanceBookedAsync(performanceId, userId); // Pass userId as well
            Debug.WriteLine($"Booking status after backend check: {isBooked}");

            // Update the IsBooked status on the model (from backend)
            performance.IsBooked = isBooked;

            if (isBooked)
            {
                performance.Message = "This performance is already booked!";
                Debug.WriteLine("Booking already exists.");
                await Shell.Current.DisplayAlert("Error", "Booking already exists.", "OK");
                return; // Stop further execution if already booked
            }
            else
            {
                var success = await _performanceService.CreateBookingAsync(performanceId);
                if (success)
                {
                    performance.IsBooked = true;  // Ensure the local state is updated after backend creation
                    performance.Message = "Booking created successfully!";
                    Debug.WriteLine("Booking created successfully.");
                    await Shell.Current.DisplayAlert("Success", "Booking created!", "OK");
                }
                else
                {
                    performance.IsBooked = false;  // Ensure the local state is updated in case of failure
                    Debug.WriteLine("Failed to create booking.");
                    await Shell.Current.DisplayAlert("Error", "Failed to create booking.", "OK");
                }
            }
        }



    }
}
