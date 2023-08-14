using Domain.Models;

namespace Lynx.Views;

public partial class DeparturePage : ContentPage
{
    DepartureViewModel ViewModel ;
	public DeparturePage( DepartureViewModel  vm)
	{
		InitializeComponent();
		BindingContext = ViewModel = vm;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await ViewModel.LoadDataAsync("api/departure/all");
    }
}	