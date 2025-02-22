using ConcertApp.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.Services
{
    public interface IPerformanceService
    {
        Task<List<Performance>> GetPerformancesByConcertIdAsync(int concertId);
        Task<bool> CreateBookingAsync(int performanceId);
        Task<bool> DeleteBookingAsync(int performanceId);
        Task<bool> IsPerformanceBookedAsync(int performanceId);
    }
}
