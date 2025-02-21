using ConcertApp.MAUI.Models;
using ConcertApp.MAUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.ViewModels
{
    public partial class PerformanceViewModel
    {
        private readonly PerformanceService _performanceService;
        public ObservableCollection<Performance> Performances { get; set; }

        public PerformanceViewModel(int concertId)
        {
            _performanceService = new PerformanceService();
            Performances = new ObservableCollection<Performance>();
            LoadPerformances(concertId);
        }

        private async void LoadPerformances(int concertId)
        {
            var performances = await _performanceService.GetPerformancesByConcertIdAsync(concertId);
            foreach (var performance in performances)
            {
                Performances.Add(performance);
            }
        }
    }
}
