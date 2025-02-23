

using ConcertApp.MAUI.Models;
using ConcertApp.MAUI.ViewModels;

namespace ConcertApp.MAUI.Views;

public partial class PerformancePage : ContentPage
{
    private readonly PerformanceViewModel _viewModel;
    public PerformancePage(PerformanceViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    
}