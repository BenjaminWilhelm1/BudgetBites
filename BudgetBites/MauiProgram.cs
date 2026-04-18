using Microsoft.Extensions.Logging;
using BudgetBites.Services;
using BudgetBites.ViewModels;
using BudgetBites.Pages;

namespace BudgetBites;

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

        builder.Services.AddSingleton<GroceryRepository>();
        builder.Services.AddSingleton<PantryRepository>();
        builder.Services.AddSingleton<SpendingRepository>();

        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<GroceryListViewModel>();
        builder.Services.AddTransient<AddGroceryItemViewModel>();
        builder.Services.AddTransient<PantryViewModel>();
        builder.Services.AddTransient<AddPantryItemViewModel>();
        builder.Services.AddTransient<BudgetViewModel>();

        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<GroceryPage>();
        builder.Services.AddTransient<AddGroceryItemPage>();
        builder.Services.AddTransient<PantryPage>();
        builder.Services.AddTransient<AddPantryItemPage>();
        builder.Services.AddTransient<BudgetPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}