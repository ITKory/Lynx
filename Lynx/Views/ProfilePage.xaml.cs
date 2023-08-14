namespace Lynx.Views;

public partial class ProfilePage : ContentPage
{
    ProfileViewModel viewModel;
	public ProfilePage(ProfileViewModel vm)
	{
		InitializeComponent();
		BindingContext = viewModel = vm;
	}
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        viewModel.LoadProfile();
    }
}
