﻿
namespace Lynx.Views;

public partial class StatsPage : ContentPage
{
	public StatsPage(StatsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
