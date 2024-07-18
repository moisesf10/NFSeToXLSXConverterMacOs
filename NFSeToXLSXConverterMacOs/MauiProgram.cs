using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Logging;
using NFSeToXLSXConverterMacOs.Domain;

namespace NFSeToXLSXConverterMacOs
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
                .UseMauiCommunityToolkit();
            //.ConfigureFonts(fonts =>
            //{
            //	fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            //});

            builder.Services.AddSingleton<MainApp>();
            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
            builder.Services.AddSingleton<Excel>();
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
