using ConcertApp.MAUI.ViewModels;

namespace ConcertApp.MAUI.Views;

public partial class ConcertPage : ContentPage
{
	public ConcertPage(ConcertViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}