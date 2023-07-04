using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.ViewModels;

[QueryProperty(nameof(SelectedDeparture), "selectedDeparture")]

public partial class DepartureDetailViewModel:BaseViewModel
{
    [ObservableProperty]
    SearchDeparture selectedDeparture;

    [RelayCommand]
    private async void JoinDeparture()
    {
        await Shell.Current.GoToAsync(nameof(QRCodePage), true, new Dictionary<string, object>
            {
                { "Departure", SelectedDeparture }
            });
    }
}
