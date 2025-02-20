using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.MAUI
{
    public static class Constants
    {
        public static string LocalHostUrl = 
            DeviceInfo.Platform == DevicePlatform.Android 
            ? (DeviceInfo.DeviceType == DeviceType.Physical ? "83.253.102.197" : "10.0.2.2")
            : "localhost";
        public static string Scheme = "https";
        public static string Port = "5001"; //5000
        public static string BookingUrl = $"{Scheme}://{LocalHostUrl}:{Port}/api/booking/{{0}}";
        public static string ConcertUrl = $"{Scheme}://{LocalHostUrl}:{Port}/api/concert/{{0}}";
        public static string PerformanceUrl = $"{Scheme}://{LocalHostUrl}:{Port}/api/performance/{{0}}";
        public static string UserUrl = $"{Scheme}://{LocalHostUrl}:{Port}/api/user/{{0}}";
    }
}
