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
    
}