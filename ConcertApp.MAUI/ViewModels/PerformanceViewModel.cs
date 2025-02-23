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
            foreach (var performance in performances)
            {
                bool isBooked = await _performanceService.IsPerformanceBookedAsync(performance.ID);
                performance.IsBooked = isBooked; // Ensure the model has this property
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

            // Check if already booked
            Debug.WriteLine($"Current booking status: {performance.IsBooked}");
            var isBooked = await _performanceService.IsPerformanceBookedAsync(performanceId);
            Debug.WriteLine($"After backend call - isBooked: {isBooked}");
            // Update the performance model's IsBooked property based on the response
            performance.IsBooked = isBooked;

            // Log the status to verify the correct value after checking with the backend
            Debug.WriteLine($"Is the performance booked? {isBooked}");

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
                    performance.IsBooked = true; // Make sure we update this after successful creation
                    performance.Message = "Booking created successfully!";
                    Debug.WriteLine("Booking created successfully.");
                    await Shell.Current.DisplayAlert("Success", "Booking created!", "OK");
                }
                else
                {
                    Debug.WriteLine("Failed to create booking.");
                    await Shell.Current.DisplayAlert("Error", "Failed to create booking.", "OK");
                }
            }
        }



    }
}
