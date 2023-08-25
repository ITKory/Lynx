using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using Lynx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UraniumUI.Icons.MaterialIcons;

namespace Lynx.ViewModels
{
    public partial class StatsViewModel:BaseViewModel
    {
        [ObservableProperty]
        List<StatItemModel> statItems;

        public StatsViewModel()
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
        async void GoToDetails()
        {
            await Shell.Current.GoToAsync(nameof(StatsDetailPage),true);
        }
    }
}
