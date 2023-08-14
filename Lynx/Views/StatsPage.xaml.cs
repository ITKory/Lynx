using UraniumUI.Pages;

namespace Lynx.Views;

public partial class StatsPage : UraniumContentPage
{
    StatsViewModel viewModel;

    public StatsPage(StatsViewModel vm)
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
