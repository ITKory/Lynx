using Lynx.Models;
using Lynx.Service;

namespace Lynx.ViewModels;

public partial class HubViewModel : BaseOptionForCollectionViewModel
{
    public HubViewModel(LynxApi lynxApi) : base(lynxApi)
    {
        Title = "Hub";
    }
    private string searchText;
    public string SearchText
    {
        get => searchText;
        set
        {
            searchText = value;
            if (searchText.Length > 0)
                Search();
            else
                Task.Run(async () => await LoadDataAsync("api/departure/all"));
        }
    }
    [RelayCommand]
    private void Search()
    {
        var disableFounds = DisableItems.Where(d => d.Title.Contains(SearchText)).ToList();
        var activeFounds = ActiveItems.Where(d => d.Title.Contains(SearchText)).ToList();

        if (disableFounds.Count > 0)
        {
            DisableItems.Clear();
            foreach (var item in disableFounds)
                DisableItems.Add(item);
        }
        if (activeFounds.Count > 0)
        {
            ActiveItems.Clear();
            foreach (var item in activeFounds)
                ActiveItems.Add(item);
        }
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
    [RelayCommand]
    private async void CloseDeparture(ListItemModel item)
    {
        bool accept = await Shell.Current.DisplayAlert("Close departure?", "Are you sure?", "Yes", "No");
        if (accept)
        {
            item.IsFound = await Shell.Current.DisplayAlert("Closing", "Person is found?", "Yes", "No");
            if (item.IsFound)
            {
                item.IsDied = await Shell.Current.DisplayAlert("Closing", "Person is died?", "Yes", "No");

            }
            item.IsActive = false;

            try
            {
               await lynxService.UpdateDepartureListItemAsync(item);

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
            ActiveItems.Remove(item);
            await LoadDataAsync("api/departure/all");
        }

    }
    [RelayCommand]
    private async void DeleteDeparture(ListItemModel item)
    {
        bool accept = await Shell.Current.DisplayAlert("remove departure?", "Are you sure?", "Yes", "No");
        if (accept)
        {
            if (item.IsActive)
                ActiveItems.Remove(item);
            else
                DisableItems.Remove(item);
            await lynxService.RemoveDepartureListItemByIdAsync(item.Id);
            await LoadDataAsync("api/departure/all");
        }
    }

    [RelayCommand]
    private async void GoToRequests()
    {
        var items = await lynxService.GetDataListAsync<ListItemModel>("api/request/get/all");

        await Shell.Current.GoToAsync(nameof(RequestsPage), true, new Dictionary<string, object>
            {
                { "Requests", items   }
            });

    }
    [RelayCommand]
    private async void GoToCrews()
    {
        await Shell.Current.GoToAsync($"{nameof(CrewsPage)}", true);
    }
    [RelayCommand]
    private async void GoToStats()
    {
        await Shell.Current.GoToAsync($"{nameof(StatsPage)}", true);
    }
    [RelayCommand]
    private async void GoToRegister()
    {
        await Shell.Current.GoToAsync($"{nameof(RegisterPage)}", true);
    }
}
