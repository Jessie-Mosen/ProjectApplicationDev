using Microsoft.Extensions.Logging;
using task4_1.ViewModels;

namespace task4_1;

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
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<DataPage>();
		builder.Services.AddTransient<Statistics>();
		//builder.Services.AddTransient<InvestmentForecast>();
		builder.Services.AddTransient<QuerryPage>();
		builder.Services.AddTransient<InsertAnimal>();
		builder.Services.AddTransient<DeleteAnimal>();
		builder.Services.AddTransient<UpdateAnimal>();
;        return builder.Build();
	}
}
