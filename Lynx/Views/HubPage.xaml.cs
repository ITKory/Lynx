using UraniumUI.Pages;

namespace Lynx.Views;

public partial class HubPage : UraniumContentPage
{
    HubViewModel viewModel;

    public HubPage(HubViewModel vm)
	{
		InitializeComponent();
		BindingContext = viewModel = vm;
	}
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await viewModel.LoadDataAsync("api/departure/all");
    }
}
