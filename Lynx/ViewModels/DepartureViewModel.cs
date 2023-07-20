using Domain.Models;
using Lynx.Models;
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
        readonly LynxApi  _lynxService;
        public ObservableCollection<DepartureListItemModel> OpenDepartures { get; } = new();
        public ObservableCollection<DepartureListItemModel> CloseDepartures { get; } = new();

        public DepartureViewModel(LynxApi  lynxApi )
        {
            Title = "Departures";
            _lynxService = lynxApi;
        }
        [ObservableProperty]
        bool isRefresh;

    

        [RelayCommand]
        public async Task LoadMore()
        {
          var items = await _lynxService.GetRefreshDataListAsync<DepartureListItemModel>(new Uri("http://10.0.2.2:5008/api/departure/all"));

            foreach (var item in items)
            {
                OpenDepartures.Add(item);
            }
        }

        public async Task LoadDataAsync()
        {
            var departureList = await _lynxService.GetRefreshDataListAsync<DepartureListItemModel>(new Uri("http://10.0.2.2:5008/api/departure/all"));

                if (OpenDepartures.Count > 0)
                OpenDepartures.Clear();

                if (CloseDepartures.Count > 0)
                CloseDepartures.Clear();

            foreach (var departure in departureList)
            {
                if (departure.IsActive)
                    OpenDepartures.Add(departure);
                else
                    CloseDepartures.Add(departure);
            }
        }

        [RelayCommand]
        private async void GoToDetails( DepartureListItemModel item)
        {
            var fullDepartureInfo = await _lynxService.GetDepartureByIdAsync(new Uri($"http://10.0.2.2:5008/api/departure/fullInfo?departureId={item.Id}"));
            if(fullDepartureInfo != null )
            {

            await Shell.Current.GoToAsync(nameof(DepartureDetailPage), true, new Dictionary<string, object>
            {
                { "selectedDeparture", fullDepartureInfo }
            });
            }
            else {
                await Shell.Current.DisplayAlert("Server error", "server error", "ok");
            }

        }

    }
}
