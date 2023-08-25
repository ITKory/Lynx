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
        Routing.RegisterRoute(nameof(RequestDetailPage), typeof(RequestDetailPage));
        Routing.RegisterRoute(nameof(QRCodePage), typeof(QRCodePage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
        Routing.RegisterRoute(nameof(CreateDeparturePage), typeof(CreateDeparturePage));
        Routing.RegisterRoute(nameof(CreateRequestPage), typeof(CreateRequestPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(HubPage), typeof(HubPage));
        Routing.RegisterRoute(nameof(StatsPage), typeof(StatsPage));
        Routing.RegisterRoute(nameof(StatsDetailPage), typeof(StatsDetailPage));
    }
}
