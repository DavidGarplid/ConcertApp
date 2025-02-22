using ConcertApp.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.Services
{
    public interface IBookingService 
    {
        Task<List<Booking>> GetBookingsByUserIdAsync(int userID);
    }
}
