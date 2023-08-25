using Domain.Models;
using Lynx.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.ViewModels
{
    public partial class CreateRequestViewModel : BaseViewModel
    {
        private LynxApi _lynxService;

        [ObservableProperty]
        string comment;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string face;
        [ObservableProperty]
        DateTime dateOfLose;
        [ObservableProperty]
        int requestAdministratorId;

        //Location Of Lose
        [ObservableProperty]
        string loseLocation;

        //Home location
        [ObservableProperty]
        string homeLocation;

        //lost profile
        [ObservableProperty]
        DateTime birthday;
        [ObservableProperty]
        string city;
        [ObservableProperty]
        string phoneNumber;
        [ObservableProperty]
        string relativesPhoneNumber;

        //informer profile
        [ObservableProperty]
        string informerName;
        [ObservableProperty]
        string informerPhoneNumber;
        [ObservableProperty]
        string informerRelativesPhoneNumber;

        [ObservableProperty]
        List<string> cities;

        public CreateRequestViewModel(LynxApi lynxApi)
        {
            _lynxService = lynxApi;
            Cities = new List<string>
            {
                 "Москва",
                 "Бор",
                 "Нижний Новгород"
            };
         

        }

        [RelayCommand]
        private async void CreateRequest()
        {
           var homeLocation = HomeLocation.Split(",", 2);
            var loseLocation = LoseLocation.Split(",", 2);
            var test_1 = DateOfLose;
            var test_2 = Birthday;
            var newSearchRequest =  new SearchRequest()
            {
                Comment = Comment,
                Date = DateOnly.FromDateTime(DateTime.Now),
                DateOfLosee = DateOnly.FromDateTime(DateOfLose),
                Face = Face,
                IsActive = true,
                IsDied = false,
                IsFound = false,
                Location = new Domain.Models.Location
                {
                    Latitude = loseLocation[0],
                    Longitude = loseLocation[1],

                },
                Lost = new Profile
                {
                    Birthday = DateOnly.FromDateTime(Birthday) ,
                    Name = Name,
                    City = new City { Title=City},
                    Location = new Domain.Models.Location
                    {
                        Latitude = homeLocation[0],
                        Longitude = homeLocation[1],

                    },
                    Phone = PhoneNumber,
                    RelativesPhone = RelativesPhoneNumber
                },
                MissingInformer = new Profile
                {
                    Name = InformerName,
                    Phone = InformerPhoneNumber,
                    RelativesPhone = InformerRelativesPhoneNumber,
                    City = new City { Title = City }

                },
                RequestAdministratorId = Convert.ToInt32(await SecureStorage.Default.GetAsync("user_id")),
            }; 
            try
            {
               await _lynxService.CreateRequestAsync(newSearchRequest);
               await Shell.Current.GoToAsync("..");
            }
            catch(  Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message,"ok");
            }

        }
    }
}
