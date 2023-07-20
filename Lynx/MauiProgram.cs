using Camera.MAUI;
using InputKit.Handlers;
using Lynx.Service;
using SkiaSharp.Views.Maui.Controls.Hosting;
using UraniumUI;
namespace Lynx;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCameraView()
			.UseMauiCommunityToolkit()
			.UseMauiMaps()
            .UseUraniumUI()
			.UseUraniumUIMaterial()
            .UseUraniumUIBlurs()
			.UseSkiaSharp()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddMaterialIconFonts();
            })
			.ConfigureMauiHandlers(handlers =>
			{

				handlers.AddInputKitHandlers();
			});
		builder.Services.AddSingleton<LynxApi>();
		builder.Services.AddSingleton<IConnectivity>((e) => Connectivity.Current);

		builder.Services.AddSingleton<StatsViewModel>();
		builder.Services.AddSingleton<StatsPage>();

		builder.Services.AddTransient<MapViewModel>();
		builder.Services.AddTransient<MapPage>();

		builder.Services.AddSingleton<HubViewModel>();
		builder.Services.AddSingleton<HubPage>();

		builder.Services.AddSingleton<ProfileViewModel>();
		builder.Services.AddSingleton<ProfilePage>();

		builder.Services.AddTransient<RequestsPage>();
		builder.Services.AddTransient<RequestsViewModel>();

        builder.Services.AddTransient<CrewsPage>();
        builder.Services.AddTransient<CrewsViewModel>();

        builder.Services.AddSingleton<DeparturePage>();
        builder.Services.AddSingleton<DepartureViewModel>();

		builder.Services.AddTransient<QRCodePage>();
		builder.Services.AddTransient<BarcodeViewModel>();

		builder.Services.AddTransient<DepartureDetailPage>();
		builder.Services.AddTransient<DepartureDetailViewModel>();

		builder.Services.AddTransient<RegisterPage>();
		builder.Services.AddTransient<RegisterViewModel>();

		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<LoginViewModel>();

		builder.Services.AddTransient<RegistrationPage>();
		builder.Services.AddTransient<RegistrationViewModel>();


        return builder.Build();
	}
}
