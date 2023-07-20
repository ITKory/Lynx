using Domain.Models;
using Lynx.Service;
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;
using static System.Formats.Asn1.AsnWriter;

namespace Lynx.ViewModels
{
    public partial class RegistrationViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        string name;
        [ObservableProperty]
        string surname;
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string phone;
        [ObservableProperty]
        string relativesPhone;
        [ObservableProperty]
        DateOnly bDay;
        [ObservableProperty]
        string call;

        [ObservableProperty]
        City selectedCity;

        [ObservableProperty]
        List<string> cities = new () ;

        [ObservableProperty]
        string password;   
        [ObservableProperty]
        string login;

        public string FullName => $"{Name} {Surname}";

       readonly LynxApi _lynxService; 
        public   RegistrationViewModel(LynxApi lynxApi)
        {
            _lynxService = lynxApi;
        }

        async public  Task LoadCities()
        {
            var cities = await _lynxService.GetRefreshDataListAsync<City>(new Uri("http://10.0.2.2:5008/api/city/all"));
            foreach (var city in cities)
                Cities.Add(city.Title);
        }

        [RelayCommand]
        private async void Registration()
        {
         
              var user = new User()
              {
                   Password = "qwe",
                   Login = "qwe",
                   Email = "qwe@gmail.com",
                   Profile = new Profile()
                   {
                       Name = "Kirpich",
                       Phone = "88005353535",
                       RelativesPhone = "228",
                       BDay = new DateOnly(111, 1, 1),
                       Call = "chert",
                       CityId = 1
                   }

        };
            await _lynxService.CreateEntityAsync(new Uri("http://10.0.2.2:5008/api/user/add"), user);

        }

        [RelayCommand]
        private async void GoToLogin()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}", true);
        }
    }
}
