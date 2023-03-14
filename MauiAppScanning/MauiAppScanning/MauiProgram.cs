using BarcodeScanner.Mobile;
using Microsoft.Extensions.Configuration;

namespace MauiAppScanning;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiMaps()            
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FontAwesome6FreeBrands.otf", "FontAwesomeBrands");
                fonts.AddFont("FontAwesome6FreeRegular.otf", "FontAwesomeRegular");
                fonts.AddFont("FontAwesome6FreeSolid.otf", "FontAwesomeSolid");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddBarcodeScannerHandler();
            });

        BarcodeScanner.Mobile.Methods.SetSupportBarcodeFormat(BarcodeFormats.Pdf417 | BarcodeFormats.QRCode);
        
        builder.Services.AddSingleton<IMediaPicker, CustomMediaPicker>();

        builder.Services.AddSingleton<MainPage, MainViewModel>();

        builder.Services.AddSingleton<ListDetailPage, ListDetailViewModel>();

        builder.Services.AddSingleton<LocalizationPage, LocalizationViewModel>();        

        builder.Services.AddSingleton<LocalDataService>();

        return builder.Build();
    }
}