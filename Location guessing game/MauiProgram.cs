using DAL;
using LocationGuesser.Models.Services;
using LocationGuesser.ViewModels;
using LocationGuesser.Views;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace LocationGuesser
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<ListService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddTransient<ScoreService>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<GameViewModel>();
            builder.Services.AddTransient<GamePage>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<ListSelectViewModel>();
            builder.Services.AddTransient<ListSelectPage>();
            builder.Services.AddTransient<ListLeaderboardViewModel>();
            builder.Services.AddTransient<ListLeaderboardPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            using var db = new GameDbContext();
            db.Database.EnsureCreated();

            return app;
        }
    }
}
