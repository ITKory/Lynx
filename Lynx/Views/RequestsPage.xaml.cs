
namespace Lynx.Views;

public partial class RequestsPage : ContentPage
{
	RequestViewModel ViewModel;
	public RequestsPage(RequestViewModel vm)
	{
		InitializeComponent();
        BindingContext = ViewModel = vm;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await ViewModel.LoadDataAsync("api/request/get/all");

    }
}