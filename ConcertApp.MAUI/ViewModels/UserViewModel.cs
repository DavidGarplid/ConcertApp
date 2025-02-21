
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertApp.Data.DTO;
using ConcertApp.MAUI.Models;
using ConcertApp.MAUI.Services;
using System.Diagnostics;

namespace ConcertApp.MAUI.ViewModels;

[ObservableObject]
public partial class UserViewModel
{
    private readonly IUserService _userService;

    [ObservableProperty]
    private User user = new();  // Initialize User to avoid null issues

    public UserViewModel(IUserService userService)
    {
        _userService = userService;
    }

    [RelayCommand]
    public async Task Login()
    {
        // Check if User object is null or properties are empty
        if (string.IsNullOrEmpty(User.Email) || string.IsNullOrEmpty(User.Password))
        {
            await Shell.Current.DisplayAlert("Error", "Please enter both email and password", "OK");
            return;
        }

        var result = await _userService.LoginAsync(User);

        if (result.Contains("Login failed"))
        {
            await Shell.Current.DisplayAlert("Error", result, "OK");
        }
        else
        {
            var (userName, userEmail, userID) = GetUserData();

            // Show the saved data on screen
            await Shell.Current.DisplayAlert("User Info", $"Name: {userName}\nEmail: {userEmail}", "OK");
            Debug.WriteLine($"Retrieved User Name: {userName}");
            Debug.WriteLine($"Retrieved User Email: {userEmail}");

            await Shell.Current.DisplayAlert("Success", "Login successful", "OK");
            await Shell.Current.GoToAsync("//MainTabBar");
            //await Shell.Current.GoToAsync("//BookingPage");

        }
    }

    private void SaveUserData(UserDto userDto)
    {
        // Save both Name and Email in Preferences
        Preferences.Set("UserName", userDto.Name);
        Preferences.Set("UserEmail", userDto.Email);
        Preferences.Set("UserID", (int)userDto.ID);

    }


    // dessa 2 är jag osäker på vart de ska vara
    public (string userName, string userEmail, int userID) GetUserData()
    {
        // Retrieve both Name and Email from Preferences
        string userName = Preferences.Get("UserName", string.Empty);  // Default value is empty string
        string userEmail = Preferences.Get("UserEmail", string.Empty);  // Default value is empty string
        int userID = Preferences.Get("UserID", 0);
        Debug.WriteLine($"Retrieved Name: {userName}");
        Debug.WriteLine($"Retrieved Email: {userEmail}");
        return (userName, userEmail, userID);
    }
    public void ClearUserData()
    {
        Preferences.Remove("UserName");
        Preferences.Remove("UserEmail");
        Preferences.Remove("UserID");
    }

}
