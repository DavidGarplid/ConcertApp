using ConcertApp.MAUI.ViewModels;

namespace ConcertApp.MAUI.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(UserViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}