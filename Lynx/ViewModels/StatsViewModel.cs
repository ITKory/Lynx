using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using Lynx.Models;
using UraniumUI.Icons.MaterialIcons;
using LiveChartsCore.Kernel.Sketches;
using Microsoft.Maui.Dispatching;
using Lynx.Service;

namespace Lynx.ViewModels;

public partial class StatsViewModel : BaseOptionForCollectionViewModel
{

    [ObservableProperty]
    List<StatItemModel> statItems;


    public StatsViewModel(LynxApi lynxApi) : base(lynxApi)
    {
        StatItems = new List<StatItemModel>()
        {
            new  StatItemModel
        {
                Title = "User",
                Icon = MaterialRegular.Add_chart,
                ChartSeries =    new ISeries[]
                              {
                                new ColumnSeries<int>
                                {
                                     Values = new [] { 6, 8, 2 },
                                     MaxBarWidth = 30,
                                }
                              },
                IsVisible = IsSeeker

    },
            new  StatItemModel
        {
                Title = "Requests ",
                Icon = MaterialRegular.List,      
                ChartSeries =    new ISeries[]
                              {
                                new ColumnSeries<int>
                                {
                                     Values = new [] { 6, 5, 8 },
                                     MaxBarWidth = 30,
                                }
                              },
                IsVisible = IsAdmin
        },
     };


    }

    [RelayCommand]
    private async void GoToDetails(ListItemModel item)
    {
        var fullDepartureInfo = await lynxService.GetDepartureByIdAsync($"api/departure/get?departureId={item.Id}");
        if (fullDepartureInfo != null)
        {
            await Shell.Current.GoToAsync(nameof(DepartureDetailPage), true, new Dictionary<string, object>
            {
                { "selectedDeparture", fullDepartureInfo }
            });
        }
        else
        {
            await Shell.Current.DisplayAlert("Server error", "server error", "ok");
        }

   }


    [RelayCommand]
    private async void GoToRequests()
    {
        var items = await lynxService.GetRefreshDataListAsync<ListItemModel>("api/request/get/all");

        await Shell.Current.GoToAsync(nameof(RequestsPage), true, new Dictionary<string, object>
            {
                { "Requests", items   }
            });

    }
    [RelayCommand]
    private async void GoToCrews()
    {
        await Shell.Current.GoToAsync($"{nameof(CrewsPage)}", true);
    }
    [RelayCommand]
    private async void GoToStats()
    {
        await Shell.Current.GoToAsync($"{nameof(DeparturePage)}", true);
    }
    [RelayCommand]
    private async void GoToRegister()
    {
        await Shell.Current.GoToAsync($"{nameof(RegisterPage)}", true);
    }
}
