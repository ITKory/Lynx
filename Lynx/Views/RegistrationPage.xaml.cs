namespace Lynx.Views;

public partial class RegistrationPage : ContentPage
{
	RegistrationViewModel ViewModel;
	public RegistrationPage(RegistrationViewModel vm)
	{
		InitializeComponent();
		BindingContext = ViewModel = vm;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		await ViewModel.LoadCities();
	}
}