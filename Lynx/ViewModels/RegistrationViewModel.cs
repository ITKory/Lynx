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
            var cities = await _lynxService.GetRefreshDataListAsync<City>("/api/city/all");
            foreach (var city in cities)
                Cities.Add(city.Title);
        }

        [RelayCommand]
        private async void Registration()
        {
         
          

 

       
          

/*            user = new User()
            {
                Password = "user2",
                Login = "user2",
                Email = "user2@gmail.com",
                Profile = new Profile()
                {
                    Name = "Gleb Larkov",
                    Phone = "89687134575",
                    RelativesPhone = "89824674187",
                    BDay = new DateOnly(2000, 4, 6),
                    Call = "Serp",
                    CityId = 1
                }

            };
              _lynxService.CreateEntityAsync(new Uri("http://10.0.2.2:5008/api/user/add"), user);*/






        }

        [RelayCommand]
        private async void GoToLogin()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}", true);
        }
    }
}
