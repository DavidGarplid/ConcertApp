using ConcertApp.MAUI.Views;

namespace ConcertApp.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ConcertPage), typeof(ConcertPage));
            Routing.RegisterRoute(nameof(PerformancePage), typeof(PerformancePage));
            Routing.RegisterRoute(nameof(BookingPage), typeof(BookingPage));
        }
    }
}
