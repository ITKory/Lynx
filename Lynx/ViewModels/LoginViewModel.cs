using Lynx.Service;
using Lynx.Models;
using Mopups.Services;
using Lynx.Controls;

namespace Lynx.ViewModels
{
    public partial class LoginViewModel :ObservableObject
    {
        private readonly LynxApi _lynxService;
        private readonly IConnectivity _connectivity;
 
        public LoginViewModel(IConnectivity connectivity, LynxApi lynxApi)
        {
        
            _lynxService = lynxApi;
            _connectivity = connectivity;
            
        }   

        [ObservableProperty]
        private string userLogin;

        [ObservableProperty]
        private bool isButtonEnabled = true;

        [ObservableProperty]
        private string userPassword;

        [RelayCommand]
        private async void Registration()
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}", true);
        }

        [RelayCommand]
        private async void Login( object e)
        {
            await MopupService.Instance.PushAsync(new LoadingMopup());
            IsButtonEnabled = false;
            if (_connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                LoggedInUserModel loggedData;
                try
                {
                      loggedData = await _lynxService.LoginUser( UserLogin, UserPassword);
                    if (loggedData != null)
                    {
                 
                        await SecureStorage.Default.SetAsync("access_token", loggedData.Token);
                        await SecureStorage.Default.SetAsync("user_id", loggedData.User.Id.ToString());
                        await SecureStorage.Default.SetAsync("roles",string.Join(";",loggedData.Roles)); 
                        await Shell.Current.GoToAsync($"//{nameof(HubPage)}", true);
                    }
                    else
                        await Shell.Current.DisplayAlert("user is hull", "user is null", "ok");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "ok");
                }
              
            }
            else
            {
                await Shell.Current.DisplayAlert("Connection lost", "Check network access and try later", "Ok");
            }
            await MopupService.Instance.PopAsync();
            IsButtonEnabled = true;
        }    
        [RelayCommand]
        private async void ForgotPassword()
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}", true);
        }
    }
}
