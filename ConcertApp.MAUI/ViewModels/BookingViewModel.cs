
using CommunityToolkit.Mvvm.ComponentModel;
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
    [ObservableObject]
    public partial class BookingViewModel 
    {
        private readonly IBookingService _bookingService;
        private readonly IPerformanceService _performanceService;

        [ObservableProperty]
        private ObservableCollection<Booking> bookings = new ObservableCollection<Booking>();  // Use Booking model

        public BookingViewModel(IBookingService bookingService, IPerformanceService performanceService)
        {
            _bookingService = bookingService;
            _performanceService = performanceService;
            LoadBookingsAsync();
        }

        

        //Fetch the bookings for the logged-in user
        public async Task LoadBookingsAsync()
        {
            try
            {
                Debug.WriteLine("Loading bookings...");
                var userData = GetUserData();
                Debug.WriteLine($"xxxxxxxxxxxxxx Retrieved User ID: {userData.userID}");
                var userID = userData.userID;

                if (userID != 0)
                {
                    var bookingsList = await _bookingService.GetBookingsByUserIdAsync(userID);
                    Debug.WriteLine($"Fetched bookings: {bookingsList.Count}");
                    bookings.Clear();

                    foreach (var booking in bookingsList)
                    {                       
                        Debug.WriteLine($"Booking: {booking.Name}, {booking.Email}");
                        bookings.Add(booking);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading bookings: {ex.Message}");
                // Optionally, show a message to the user if something goes wrong
            }
        }

        private (string userName, string userEmail, int userID) GetUserData()
        {
            string userName = Preferences.Get("UserName", string.Empty);
            string userEmail = Preferences.Get("UserEmail", string.Empty);
            int userID = Preferences.Get("UserID", 0);

            return (userName, userEmail, userID);
        }
    }
}
