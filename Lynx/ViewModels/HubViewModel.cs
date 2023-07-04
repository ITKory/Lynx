namespace Lynx.ViewModels;

public partial class HubViewModel : BaseViewModel
{


    [RelayCommand]
    private async void GoToRequests()
    {
        await Shell.Current.GoToAsync($"{nameof(RequestsPage)}", true);
    }
    [RelayCommand]
    private async void GoToCrews()
    {
        await Shell.Current.GoToAsync($"{nameof(CrewsPage)}", true);
    }
    [RelayCommand]
    private async void GoToDeparture()
    {
        await Shell.Current.GoToAsync($"{nameof(DeparturePage)}", true);
    }
}
