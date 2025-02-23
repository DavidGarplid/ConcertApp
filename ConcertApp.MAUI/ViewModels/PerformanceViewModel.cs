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
            Performances.Clear();
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

            int userId = Preferences.Get("UserID", 0);

            var isBooked = await _performanceService.IsPerformanceBookedAsync(performanceId, userId);
            performance.IsBooked = isBooked;

            if (isBooked)
            {
                
                await Shell.Current.DisplayAlert("Error", "Booking already exists.", "OK");
                return; 
            }
            else
            {
                var success = await _performanceService.CreateBookingAsync(performanceId);
                if (success)
                {
                    performance.IsBooked = true;
                   
                    await Shell.Current.DisplayAlert("Success", "Booking created!", "OK");
                }
                else
                {
                    performance.IsBooked = false;
                    await Shell.Current.DisplayAlert("Error", "Failed to create booking.", "OK");
                }
            }
        }



    }
}
