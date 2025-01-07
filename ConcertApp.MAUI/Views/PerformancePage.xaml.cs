using ConcertApp.MAUI.ViewModels;

namespace ConcertApp.MAUI.Views;

public partial class PerformancePage : ContentPage
{
	public PerformancePage(PerformanceViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}