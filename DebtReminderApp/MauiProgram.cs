using DebtReminderApp.Data;
using DebtReminderApp.ViewModels;
using DebtReminderApp.Views;
using Microsoft.Extensions.Logging;

namespace DebtReminderApp
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
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif

			//Configuration Dependency Injection
			builder.Services.AddSingleton<DatabaseContext>();
			builder.Services.AddSingleton<DebtorsViewModel>();
			builder.Services.AddSingleton<DebtorViewModel>();

			builder.Services.AddSingleton<DebtorsPage>();
			builder.Services.AddSingleton<DebtorPage>();
			builder.Services.AddSingleton<AppShell>();

			return builder.Build();
		}
	}
}
