using ConcertApp.MAUI.ViewModels;
using System.Diagnostics;

namespace ConcertApp.MAUI.Views;

public partial class BookingPage : ContentPage
{
	public BookingPage(BookingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = (BookingViewModel)BindingContext;
        await viewModel.LoadBookingsAsync();  
    }

}