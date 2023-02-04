using CarListingApp.Services;
using CarListingApp.ViewModels;
using CarListingApp.Views;
using Microsoft.Extensions.Logging;

namespace CarListingApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "cars.db3");
        builder.Services.AddSingleton<CarService>(s => ActivatorUtilities.CreateInstance<CarService>(s, dbPath));
        
        builder.Services.AddSingleton<CarListingViewModel>();
        builder.Services.AddTransient<CarDetailsViewModel>();
        
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<CarDetailsPage>();

#if DEBUG3
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}