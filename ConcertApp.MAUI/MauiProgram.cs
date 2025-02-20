//using AutoMapper;
//using CommunityToolkit.Maui;
//using Microsoft.Extensions.Logging;
//using ConcertApp.MAUI.Services;
//using ConcertApp.MAUI.Models;
//using ConcertApp.MAUI.ViewModels;
//using ConcertApp.MAUI.Views;
//using ConcertApp.MAUI;

//namespace Todo.MAUI;
//public static class MauiProgram
//{
//    public static MauiApp CreateMauiApp()
//    {
//        var builder = MauiApp.CreateBuilder();
//        builder
//            .UseMauiApp<App>()
//            .UseMauiCommunityToolkit()
//            .ConfigureFonts(fonts =>
//            {
//                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
//            });

//#if DEBUG
//        builder.Logging.AddDebug();
//#endif

//        // Register IHttpsClientHandlerService
//        builder.Services.AddSingleton<IHttpsClientHandlerService, HttpsClientHandlerService>();

//        // Register AutoMapper
//        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//        // Register RestService for each entity type
//        builder.Services.AddScoped<IRestService<Booking>>(provider =>
//            new RestService<Booking>(
//                provider.GetRequiredService<IHttpsClientHandlerService>(),
//                provider.GetRequiredService<IMapper>(),
//                "https://localhost:5001/api/booking"));

//        builder.Services.AddScoped<IRestService<Concert>>(provider =>
//            new RestService<Concert>(
//                provider.GetRequiredService<IHttpsClientHandlerService>(),
//                provider.GetRequiredService<IMapper>()));

//        builder.Services.AddScoped<IRestService<User>>(provider =>
//            new RestService<User>(
//                provider.GetRequiredService<IHttpsClientHandlerService>(),
//                provider.GetRequiredService<IMapper>()));

//        builder.Services.AddScoped<IRestService<Performance>>(provider =>
//            new RestService<Performance>(
//                provider.GetRequiredService<IHttpsClientHandlerService>(),
//                provider.GetRequiredService<IMapper>()));

//        // Register EntityService for each entity type
//        builder.Services.AddScoped<IConcertAppService<Booking>, ConcertApp.MAUI.Services.ConcertAppService<Booking>>();
//        builder.Services.AddScoped<IConcertAppService<Concert>, ConcertAppService<Concert>>();
//        builder.Services.AddScoped<IConcertAppService<User>, ConcertAppService<User>>();
//        builder.Services.AddScoped<IConcertAppService<Performance>, ConcertAppService<Performance>>();

//        // Pages
//        builder.Services.AddSingleton<LoginPage>();
//        builder.Services.AddSingleton<ConcertPage>();
//        builder.Services.AddTransient<PerformancePage>();
//        builder.Services.AddSingleton<BookingPage>();

//        // ViewModels
//        builder.Services.AddSingleton<UserViewModel>();
//        builder.Services.AddSingleton<ConcertViewModel>();
//        builder.Services.AddSingleton<PerformanceViewModel>();
//        builder.Services.AddSingleton<BookingViewModel>();

//        return builder.Build();
//    }
//}

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

        // Register RestService for each entity type
        //builder.Services.AddScoped<IRestService<Booking>>(provider =>
        //{
        //    var handler = provider.GetRequiredService<IHttpsClientHandlerService>().GetPlatformMessageHandler();
        //    var client = new HttpClient(handler);
        //    return new RestService<Booking>(client, provider.GetRequiredService<IMapper>(), "https://localhost:5001/api/booking");
        //});

        //builder.Services.AddScoped<IRestService<Concert>>(provider =>
        //{
        //    var handler = provider.GetRequiredService<IHttpsClientHandlerService>().GetPlatformMessageHandler();
        //    var client = new HttpClient(handler);
        //    return new RestService<Concert>(client, provider.GetRequiredService<IMapper>(), "https://localhost:5001/api/concert");
        //});

        //builder.Services.AddScoped<IRestService<User>>(provider =>
        //{
        //    var handler = provider.GetRequiredService<IHttpsClientHandlerService>().GetPlatformMessageHandler();
        //    var client = new HttpClient(handler);
        //    return new RestService<User>(client, provider.GetRequiredService<IMapper>(), "https://localhost:5001/api/user");
        //});

        //builder.Services.AddScoped<IRestService<Performance>>(provider =>
        //{
        //    var handler = provider.GetRequiredService<IHttpsClientHandlerService>().GetPlatformMessageHandler();
        //    var client = new HttpClient(handler);
        //    return new RestService<Performance>(client, provider.GetRequiredService<IMapper>(), "https://localhost:5001/api/performance");
        //});
        builder.Services.AddSingleton<HttpClient>();
        //builder.Services.AddScoped<HttpClient>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddSingleton<IRestService<User>, RestService<User>>();
        builder.Services.AddScoped<ConcertService>();
        builder.Services.AddScoped<PerformanceService>();
        builder.Services.AddScoped<BookingService>();

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