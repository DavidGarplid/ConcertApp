using ConcertApp.MAUI.ViewModels;

namespace ConcertApp.MAUI.Views;

public partial class BookingPage : ContentPage
{
	public BookingPage(BookingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}