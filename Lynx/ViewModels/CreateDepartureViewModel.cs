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


    public partial class CreateDepartureViewModel : BaseViewModel, IQueryAttributable
    {
        private LynxApi _lynxService;

        [ObservableProperty]
        SearchRequest selectedRequest;

        [ObservableProperty]
        bool isUrgent;

        [ObservableProperty]
        string administratorCall;

        [ObservableProperty]
        DateTime departureDate;

        [ObservableProperty]
        string cartographerCall;

        [ObservableProperty]
        List<string> calls;

        [ObservableProperty]
        string departureCoordinates;

        private List<User> users ;
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            users = (List<User>)query[nameof(users)];
            SelectedRequest = (SearchRequest)query[nameof(selectedRequest)];
            Calls = users
           .Select(u => u.Profile.Call).ToList();
        }

        public CreateDepartureViewModel(LynxApi lynxService)
        {
       
            _lynxService = lynxService;
        }
 
        [RelayCommand]
        private async void CreateDeparture()
        {
            var departureCoordinates = DepartureCoordinates.Split(",", 2);
            SearchDeparture searchDeparture = new()
            {
                SearchRequestId = SelectedRequest.Id,
                Location = new Domain.Models.Location()
                {
                    Latitude = departureCoordinates[0],
                    Longitude = departureCoordinates[1]

                },
                IsActive = true,
                IsUrgent = IsUrgent,
                SearchAdministratorId = users.First(u=>u.Profile.Call == AdministratorCall).Id,
                Date = DateOnly.FromDateTime(DepartureDate),
                CartographerId = users.First(u => u.Profile.Call == CartographerCall).Id,

            };
            try
            {
             await _lynxService.CreateDepartureAsync( searchDeparture);
            }
            catch(Exception ex) 
            {
               await Shell.Current.DisplayAlert("error", ex+"", "Ok");
            }
           await Shell.Current.GoToAsync("..");

        }
    }
}
