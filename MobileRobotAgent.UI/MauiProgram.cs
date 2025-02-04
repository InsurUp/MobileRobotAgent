using Microsoft.Extensions.Logging;
using MobileRobotAgent.UI.Services.Abstract;
using MobileRobotAgent.UI.Services.Concrete;
using MudBlazor.Services;

namespace MobileRobotAgent.UI;

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

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMudServices();

        builder.Services.AddSingleton(sp =>
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://prod-spmw.sigortapro.com/")
            };

            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        });

        // Common Services
        builder.Services.AddSingleton<ITokenService, TokenService>();
        builder.Services.AddSingleton<ISmsProcessingService, SmsProcessingService>();

        // Platform Specific Services
#if ANDROID
        builder.Services.AddSingleton<ISmsService, Platforms.Android.Services.Concrete.SmsService>();
#endif

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}