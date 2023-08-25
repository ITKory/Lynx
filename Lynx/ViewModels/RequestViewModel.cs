using Domain.Models;
using Lynx.Models;
using Lynx.Service;

namespace Lynx.ViewModels
{
    [QueryProperty(nameof(ItemsList), "Requests")]
    public partial class RequestViewModel : BaseOptionForCollectionViewModel
    {
        public RequestViewModel(LynxApi lynxApi) : base(lynxApi)
        {
            Title = "Requests";
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
        private async void RefreshData()
        {
            await LoadDataAsync("api/request/get/all");
        }

        [RelayCommand]
        private async void GoToDetails(ListItemModel item)
        {
            var fullRequestInfo = await lynxService.GetRequestByIdAsync(item.Id);
            if (fullRequestInfo != null)
            {
                await Shell.Current.GoToAsync(nameof(RequestDetailPage), true, new Dictionary<string, object>
            {
                { "selectedRequest", fullRequestInfo }
            });
            }
            else
            {
                await Shell.Current.DisplayAlert("Server error", "server error", "ok");
            }

        }
        [RelayCommand]
        private async void GoToCreateRequest()
        {
            await Shell.Current.GoToAsync(nameof(CreateRequestPage), true);
        }

        [RelayCommand]
        private async void CloseRequest(ListItemModel item)
        {

            bool accept = await Shell.Current.DisplayAlert("Close request?", "Are you sure?", "Yes", "No");
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
                    await lynxService.UpdateRequestListItemAsync(item);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                }
                ActiveItems.Remove(item);
                await LoadDataAsync("api/request/get/all");
            }

        }
        [RelayCommand]
        private async void RemoveRequest(ListItemModel item)
        {
            bool accept = await Shell.Current.DisplayAlert("Close request?", "Are you sure?", "Yes", "No");
            if (accept)
            {
                if (item.IsActive)
                    ActiveItems.Remove(item);
                else
                    DisableItems.Remove(item);
                await lynxService.RemoveDepartureListItemByIdAsync(item.Id);
                await LoadDataAsync("api/request/get/all");
            }

        }

        [RelayCommand]
        private async void CreateDeparture(ListItemModel item)
        {
            bool accept = await Shell.Current.DisplayAlert("Create departure?", "Are you sure?", "Yes", "No");
            if (accept)
            {
                var searchRequest = await lynxService
                       .GetRequestByIdAsync(item.Id);

                var users = await lynxService
               .GetDataListAsync<User>("api/user/all");

                await Shell.Current.GoToAsync(nameof(CreateDeparturePage), true, new Dictionary<string, object>
            {
                { "selectedRequest", searchRequest },
                    {"users" , users}
            });

            }
        }
        [RelayCommand]
        private async void OpenRequest(ListItemModel item)
        {
            bool accept = await Shell.Current.DisplayAlert("Open request?", "Are you sure?", "Yes", "No");
            if (accept)
            {
                item.IsActive = true;
                item.IsFound = false;
                item.IsDied = false;

                try
                {
                    await lynxService.UpdateRequestListItemAsync(item);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                }
                DisableItems.Remove(item);
                await LoadDataAsync("api/request/get/all");
            }
        }


    }
}
