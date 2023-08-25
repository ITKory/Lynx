using Domain.Models;
using Lynx.Controls;
using Lynx.Models;
using Lynx.Service;
using Mopups.Services;

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
        DateTime birthday;
        [ObservableProperty]
        string call;
        [ObservableProperty]
        City selectedCity;
        [ObservableProperty]
        List<string> cities = new();
        [ObservableProperty]
        string password;
        [ObservableProperty]
        string login;
        [ObservableProperty]
        string errorText;
        [ObservableProperty]
        bool errorVisible;
        [ObservableProperty]
        string city;
        public string FullName => $"{Name} {Surname}";

        readonly LynxApi _lynxService;
        public RegistrationViewModel(LynxApi lynxApi)
        {
            _lynxService = lynxApi;
        }
        async public Task LoadCities()
        {
            var cities = await _lynxService.GetDataListAsync<City>("api/city/all");
            foreach (var city in cities)
                Cities.Add(city.Title);
        }

        [RelayCommand]
        private async void Registration()
        {
            var user = new User()
            {
                Password = Password,
                Login = Login,
                Email = Email,
                Profile = new Profile()
                {
                    Name = FullName,
                    Phone = Phone,
                    RelativesPhone = RelativesPhone,
                    Birthday = DateOnly.FromDateTime(Birthday),
                    Call = Call,
                    City = new City { Title = City }
                }
            };

            try
            {
               var newUser =  await _lynxService.CreateUser(user) ?? throw new Exception("user is null");

                await MopupService.Instance.PushAsync(new LoadingMopup());
                var loggedData = await _lynxService.LoginUser(  Login, Password);
                if (loggedData != null)
                {
                    await SecureStorage.Default.SetAsync("access_token", loggedData.Token);
                    await SecureStorage.Default.SetAsync("user_id", loggedData.User.Id.ToString());
                    await SecureStorage.Default.SetAsync("roles", string.Join(";", loggedData.Roles));

                    await Shell.Current.GoToAsync($"//{nameof(HubPage)}", true);
                }
                else
                {
                    throw new Exception("login error");
                }
                await MopupService.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                ErrorVisible = true;
            }
        }

        [RelayCommand]
        private async void GoToLogin()
        {
            await Shell.Current.GoToAsync($"..", true);
        }
    }
}
