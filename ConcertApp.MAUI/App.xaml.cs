using System.Diagnostics;

namespace ConcertApp.MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            try
            {
                MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"App Startup Error: {ex.Message}");
            }
        }
    }
}
