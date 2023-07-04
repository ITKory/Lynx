using UraniumUI.Pages;

namespace Lynx.Views;

public partial class HubPage : UraniumContentPage
{
	public HubPage(HubViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
