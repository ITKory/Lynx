namespace Lynx;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(RequestsPage), typeof(RequestsPage));
        Routing.RegisterRoute(nameof(DeparturePage), typeof(DeparturePage));
        Routing.RegisterRoute(nameof(CrewsPage), typeof(CrewsPage));
        Routing.RegisterRoute(nameof(DepartureDetailPage), typeof(DepartureDetailPage));
        Routing.RegisterRoute(nameof(QRCodePage), typeof(QRCodePage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(StatsPage), typeof(StatsPage));
    }
}
