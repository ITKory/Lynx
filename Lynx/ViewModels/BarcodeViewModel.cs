using Camera.MAUI;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camera.MAUI.ZXingHelper;


namespace Lynx.ViewModels;

[QueryProperty(nameof(BarCodeContent), "selectedDeparture")]
public partial class BarcodeViewModel : BaseViewModel
{
    [ObservableProperty]
    SearchDeparture departure;

    [ObservableProperty]
    string barCodeContent;
}
