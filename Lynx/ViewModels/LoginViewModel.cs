using Lynx.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly LynxApi _lynxService;
        private readonly IConnectivity _connectivity;
        public LoginViewModel(IConnectivity connectivity, LynxApi lynxApi)
        {
            Title = "Login Page";
            _lynxService = lynxApi;
            _connectivity = connectivity;
        }

        [ObservableProperty]
        private string userLogin;

        [ObservableProperty]
        private string userPassword;

        [RelayCommand]
        private async void Registration()
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}", true);
        }

        [RelayCommand]
        private async void Login()
        {
            if (_connectivity.NetworkAccess == NetworkAccess.Internet)
            {//try catch
                var loggedData = await _lynxService.LoginUser(new Uri("http://10.0.2.2:5008/api/user/login"), UserLogin, UserPassword);
                if (loggedData != null)
                    await Shell.Current.GoToAsync($"//{nameof(StatsPage)}", true);
                else
                    await Shell.Current.DisplayAlert("user is hull", "user is null", "ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Connection lost", "Check network access and try later", "Ok");
            }
        }    
        [RelayCommand]
        private async void ForgotPassword()
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}", true);
        }
    }
}
