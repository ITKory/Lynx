using Camera.MAUI;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camera.MAUI.ZXingHelper;


namespace Lynx.ViewModels;
[QueryProperty(nameof(Departure), "Departure")]
public partial class BarcodeViewModel : ObservableObject
{
    [ObservableProperty]
    SearchDeparture departure;

    [ObservableProperty]
    string test;

    [ObservableProperty]
    string barCodeContent;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NumCameras))]
    private CameraInfo camera ;

    [ObservableProperty]
    private ObservableCollection<CameraInfo> cameras = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SetNumCamCommand))]
    int numCameras;



    [RelayCommand]
    private void SetNumCam()
    {
        if (NumCameras > 0)
            Camera = Cameras.First();
    }

    [ObservableProperty]

    BarcodeDecodeOptions barCodeOptions;
    public string BarcodeText { get; set; } = "No barcode detected";
    private ZXing.Result[] barCodeResults;
    public ZXing.Result[] BarCodeResults
    {
        get => barCodeResults;
        set
        {
            barCodeResults = value;
            if (barCodeResults != null && barCodeResults.Length > 0)
                BarcodeText = barCodeResults[0].Text;
            else
                BarcodeText = "No barcode detected";
            OnPropertyChanged(nameof(BarcodeText));
        }
    }
    public BarcodeViewModel()
    {
        BarCodeOptions = new Camera.MAUI.ZXingHelper.BarcodeDecodeOptions
        {
            AutoRotate = true,
            PossibleFormats = { ZXing.BarcodeFormat.QR_CODE },
            ReadMultipleCodes = false,
            TryHarder = true,
            TryInverted = true
        };
        string test_0 = test;
        string test_1 = Newtonsoft.Json.JsonConvert.SerializeObject(Departure);
        string test_2 = Newtonsoft.Json.JsonConvert.SerializeObject(Departure).Normalize();
        string test_3 = Newtonsoft.Json.JsonConvert.SerializeObject(Departure).ToString();
        string test_4 = Newtonsoft.Json.JsonConvert.SerializeObject(Departure).Normalize().ToString();


        BarCodeContent = Newtonsoft.Json.JsonConvert.SerializeObject(Departure).Normalize();

    }
}
