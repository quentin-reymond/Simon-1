using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace Simon;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseMauiApp<App>() // ← Ceci doit être AVANT
			.UseMauiCommunityToolkitMediaElement() // ← Et ça ensuite
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

#if DEBUG
		
#endif

		return builder.Build();
	}
}
