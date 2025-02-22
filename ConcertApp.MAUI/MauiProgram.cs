
using AutoMapper;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ConcertApp.MAUI.Services;
using ConcertApp.MAUI.Models;
using ConcertApp.MAUI.ViewModels;
using ConcertApp.MAUI.Views;
using ConcertApp.MAUI;
using System.Net.Http;

namespace ConcertApp.MAUI;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Register IHttpsClientHandlerService
        builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();

        // Register AutoMapper
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        
        builder.Services.AddSingleton<HttpClient>();
        //builder.Services.AddScoped<HttpClient>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddSingleton<IRestService<User>, RestService<User>>();
        builder.Services.AddScoped<IConcertService, ConcertService>();
        builder.Services.AddScoped<IPerformanceService, PerformanceService>();
        builder.Services.AddScoped <IBookingService, BookingService > ();

        // Pages
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<ConcertPage>();
        builder.Services.AddTransient<PerformancePage>();
        builder.Services.AddSingleton<BookingPage>();

        // ViewModels
        builder.Services.AddSingleton<UserViewModel>();
        builder.Services.AddSingleton<ConcertViewModel>();
        builder.Services.AddSingleton<PerformanceViewModel>();
        builder.Services.AddSingleton<BookingViewModel>();

        return builder.Build();
    }
}