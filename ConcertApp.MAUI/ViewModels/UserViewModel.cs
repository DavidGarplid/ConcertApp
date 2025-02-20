using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertApp.MAUI.Models;
using ConcertApp.MAUI.Services;

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
        if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
        {
            await Shell.Current.DisplayAlert("Error", "Please enter both email and password", "OK");
            return;
        }

        var result = await _userService.LoginAsync(user);

        if (result.Contains("Login failed"))
        {
            await Shell.Current.DisplayAlert("Error", result, "OK");
        }
        else
        {
            await Shell.Current.DisplayAlert("Success", "Login successful", "OK");
            // Navigate to home or save token
        }
    }
}
