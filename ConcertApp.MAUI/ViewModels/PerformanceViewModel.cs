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



        //[RelayCommand]
        public async Task ToggleBooking(int performanceId, bool isToggled)
        {
            if (isToggled)
            {
                bool success = await _performanceService.CreateBookingAsync(performanceId);
                if (success)
                {
                    Debug.WriteLine("Booking created successfully!");
                    await Shell.Current.DisplayAlert("Success", "Booking created!", "OK");
                }
                else
                {
                    Debug.WriteLine("Failed to create booking.");
                    await Shell.Current.DisplayAlert("Error", "Failed to create booking.", "OK");
                }
            }
            else
            {
                bool success = await _performanceService.DeleteBookingAsync(performanceId);
                if (success)
                {
                    Debug.WriteLine("Booking deleted successfully!");
                    await Shell.Current.DisplayAlert("Success", "Booking deleted!", "OK");
                }
                else
                {
                    Debug.WriteLine("Failed to delete booking.");
                    await Shell.Current.DisplayAlert("Error", "Failed to delete booking.", "OK");
                }
            }
        }



    }
}
