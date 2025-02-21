using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.ViewModels
{
    public partial class ConcertViewModel
    {




        [RelayCommand]
        public async Task Logout()
        {
            Preferences.Clear(); 
            await Shell.Current.GoToAsync("//LoginPage"); 
        }
    }
}
