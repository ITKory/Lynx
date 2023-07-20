
using UraniumUI.Pages;

namespace Lynx.Views;

public partial class StatsPage : UraniumContentPage
{
	public StatsPage(StatsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
