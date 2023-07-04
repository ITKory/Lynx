using Domain.Models;
using Lynx.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.ViewModels
{
   public partial class DepartureViewModel :BaseViewModel
    {
        readonly LynxApi _lynxService;

      

        public ObservableCollection<SearchDeparture> Departures { get; } = new();

        public DepartureViewModel(LynxApi lynxApi )
        {
            _lynxService = lynxApi;

           
        }
        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        private async void OnRefreshing()
        {
            IsRefreshing = true;

            try
            {
                await LoadDataAsync();
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        public async Task LoadMore()
        {
          var items = await _lynxService.RefreshDataAsync();

            foreach (var item in items)
            {
                Departures.Add(item);
            }
        }

        public async Task LoadDataAsync()
        {
            var departureList = await _lynxService.RefreshDataAsync();

            if (Departures.Count > 0)
                Departures.Clear();

            foreach (var departure in departureList)
                Departures.Add(departure);
        }

        [RelayCommand]
        private async void GoToDetails(SearchDeparture item)
        {
           await Shell.Current.GoToAsync(nameof(DepartureDetailPage), true, new Dictionary<string, object>
            {
                { "selectedDeparture", item }
            });
        }


    }
}
