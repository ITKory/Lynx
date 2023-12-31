﻿namespace Lynx.Views;

public partial class MapPage : ContentPage
{
    public MapViewModel viewModel;
	public MapPage(MapViewModel vm)
	{
		InitializeComponent();
		BindingContext = viewModel = vm;
 
	}
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await viewModel.LoadDataAsync("api/departure/all");
        viewModel.LoadMap();
    }
}
