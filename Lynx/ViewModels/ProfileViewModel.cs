using Domain.Models;
using Lynx.Service;

namespace Lynx.ViewModels;

public partial class ProfileViewModel : BaseViewModel
{
    [ObservableProperty]
    private Domain.Models.Profile userProfile;

    private LynxApi _lynxService;
    public ProfileViewModel()
    {
        _lynxService = new();
    }

    public async void LoadProfile()
    {
        var userId = await SecureStorage.Default.GetAsync("user_id");
        UserProfile =  await _lynxService.GetProfileAsync($"api/profile/get?userId={userId}");
    }

    [RelayCommand]
    public async void Logout()
    {
        bool isCancel = await Shell.Current.DisplayAlert("Question?", "Are you sure ?", "Yes", "No");
        if (isCancel) {
            SecureStorage.Default.Remove("access_token");
            SecureStorage.Default.Remove("user_id");
            Application.Current.MainPage = new AppShell();
           }
    }

}
