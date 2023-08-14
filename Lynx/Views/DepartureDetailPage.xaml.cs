namespace Lynx.Views;

public partial class DepartureDetailPage : ContentPage
{
	DepartureDetailViewModel ViewModel;
    public DepartureDetailPage(DepartureDetailViewModel vm)
	{
		InitializeComponent();
        BindingContext = ViewModel = vm;
    }
    protected   override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        ViewModel.LoadParticipantsCommand.Execute(null);
        ViewModel.LoadMapCommand.Execute(null);
    }
}