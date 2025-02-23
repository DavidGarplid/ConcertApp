using ConcertApp.MAUI.ViewModels;
using System.Diagnostics;

namespace ConcertApp.MAUI.Views;

public partial class ConcertPage : ContentPage
{
	public ConcertPage(ConcertViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        Debug.WriteLine($"BindingContext: {BindingContext?.GetType().Name}");
    }
}