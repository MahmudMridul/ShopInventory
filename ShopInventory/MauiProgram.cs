using Microsoft.Extensions.Logging;
using ShopInventory.Services;
using ShopInventory.Views;
using ShopInventory.ViewModels;

namespace ShopInventory
{
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
                });

            //builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddLogging(configure => configure.AddDebug());
#endif

            // Register services
            builder.Services.AddSingleton<DatabaseService>();

            // Register ViewModels
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<PurchasedItemsViewModel>();
            builder.Services.AddTransient<SoldItemsViewModel>();
            builder.Services.AddTransient<AddEditPurchasedItemViewModel>();
            builder.Services.AddTransient<AddEditSoldItemViewModel>();

            // Register Views
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<PurchasedItemsPage>();
            builder.Services.AddTransient<SoldItemsPage>();
            builder.Services.AddTransient<AddEditPurchasedItemPage>();
            builder.Services.AddTransient<AddEditSoldItemPage>();

            return builder.Build();
        }
    }
}