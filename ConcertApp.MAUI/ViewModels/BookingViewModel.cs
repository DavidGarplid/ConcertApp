
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
    [ObservableObject]
    public partial class BookingViewModel 
    {
        private readonly IBookingService _bookingService;

        [ObservableProperty]
        private ObservableCollection<Booking> bookings = new ObservableCollection<Booking>();  // Use Booking model

        public BookingViewModel(IBookingService bookingService, IPerformanceService performanceService)
        {
            _bookingService = bookingService;
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

        [RelayCommand]
        private async Task DeleteBooking(int bookingId)
        {
            Debug.WriteLine($"DeleteBookingCommand triggered for Booking ID: {bookingId}");

            // Confirm with the user before deleting
            bool isConfirmed = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to delete this booking?", "Yes", "No");

            if (!isConfirmed)
                return;

            // Call the service to delete the booking
            bool success = await _bookingService.DeleteBookingAsync(bookingId);
            if (success)
            {
                // Find and remove the booking from the ObservableCollection
                var bookingToRemove = Bookings.FirstOrDefault(b => b.ID == bookingId);
                if (bookingToRemove != null)
                {
                    Bookings.Remove(bookingToRemove);
                }

                // Inform the user about success
                await Shell.Current.DisplayAlert("Success", "Booking deleted.", "OK");
            }
            else
            {
                // If deletion failed, notify the user
                await Shell.Current.DisplayAlert("Error", "Failed to delete booking.", "OK");
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
