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
    public partial class DepartureViewModel : BaseOptionForCollectionViewModel
    {
        public DepartureViewModel(LynxApi lynxApi) : base(lynxApi)
        {
            Title = "Departures";
        }

        [RelayCommand]
        private async void GoToDetails(ListItemModel item)
        {
            var fullDepartureInfo = await lynxService.GetDepartureByIdAsync(item.Id);
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

    }
}
