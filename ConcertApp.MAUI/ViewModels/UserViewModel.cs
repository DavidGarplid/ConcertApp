
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
    private User user = new();

    public UserViewModel(IUserService userService)
    {
        _userService = userService;
    }

    [RelayCommand]
    public async Task Login()
    {
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

            
            await Shell.Current.GoToAsync("//MainTabBar");


        }
    }



    public (string userName, string userEmail, int userID) GetUserData()
    {

        string userName = Preferences.Get("UserName", string.Empty);  
        string userEmail = Preferences.Get("UserEmail", string.Empty);  
        int userID = Preferences.Get("UserID", 0);

        return (userName, userEmail, userID);
    }
    public void ClearUserData()
    {
        Preferences.Remove("UserName");
        Preferences.Remove("UserEmail");
        Preferences.Remove("UserID");
    }

}
