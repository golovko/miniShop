using Microsoft.Extensions.Logging;
using App.Services;
using App.ViewModel;
using App.View;

namespace App;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<ProductsService>();
        builder.Services.AddSingleton<PeopleService>();
        builder.Services.AddSingleton<OrdersService>();
        builder.Services.AddSingleton<AuthService>();

        builder.Services.AddTransient<MainPage>();

        builder.Services.AddTransient<OrdersView>();
        builder.Services.AddTransient<OrdersViewModel>();
        builder.Services.AddTransient<OrderDetailsView>();
        builder.Services.AddTransient<OrderDetailsViewModel>();

        builder.Services.AddTransient<PeopleListView>();
        builder.Services.AddTransient<PeopleListViewModel>();
        builder.Services.AddTransient<PersonDetailsView>();
        builder.Services.AddTransient<PersonDetailsViewModel>();
        builder.Services.AddTransient<AddPersonPage>();
        builder.Services.AddTransient<AddPersonViewModel>();

        builder.Services.AddTransient<SettingsView>();
        builder.Services.AddTransient<SettingsViewModel>();
        
        builder.Services.AddTransient<ProductsViewModel>();
        builder.Services.AddTransient<ProductsView>();
        builder.Services.AddTransient<ProductDetailsViewModel>();
        builder.Services.AddTransient<ProductDetailsView>();
        
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<LoginView>();

        builder.Services.AddTransient<CartViewModel>();
        builder.Services.AddTransient<CartView>();

        return builder.Build();
    }
}

