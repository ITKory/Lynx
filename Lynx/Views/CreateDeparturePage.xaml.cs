namespace Lynx.Views;

public partial class CreateDeparturePage : ContentPage
{
	public CreateDeparturePage(CreateDepartureViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}