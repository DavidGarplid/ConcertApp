using CommunityToolkit.Mvvm.Input;
using ConcertApp.MAUI.Models;
using ConcertApp.MAUI.Services;
using ConcertApp.MAUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConcertApp.MAUI.ViewModels
{
    public partial class ConcertViewModel
    {




        [RelayCommand]
        public async Task Logout()
        {
            Preferences.Clear(); 
            await Shell.Current.GoToAsync("//LoginPage"); 
        }
        private readonly ConcertService _concertService;
        
        
        public ObservableCollection<Concert> Concerts { get; set; }

        public ConcertViewModel()
        {
            _concertService = new ConcertService();
            Concerts = new ObservableCollection<Concert>();
            Debug.WriteLine($"NavigateToPerformancesCommand is null: {NavigateToPerformancesCommand == null}");
            LoadConcerts();
        }

        private async void LoadConcerts()
        {
            var concerts = await _concertService.GetAllConcertsAsync();
            foreach (var concert in concerts)
            {
                Concerts.Add(concert);
            }
        }
        [RelayCommand]
        private async Task NavigateToPerformances(Concert concert)
        {
            Debug.WriteLine("Pressed");
            await Shell.Current.GoToAsync($"PerformancePage", new Dictionary<string, object>
            {
                { "concertId", concert.ID }
            });
        }
    }
}
