using Camera.MAUI;
using Camera.MAUI.ZXingHelper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace Lynx.ViewModels
{
    public partial class RegisterViewModel:BaseViewModel
    {
        JsonSerializerOptions _serializerOptions;

        [ObservableProperty]
        BarcodeDecodeOptions barCodeOptions;
        [ObservableProperty]
        SearchDeparture departure;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NumCameras))]
        private CameraInfo camera;


        [ObservableProperty]
        private ObservableCollection<CameraInfo> cameras = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SetNumCamCommand))]
        int numCameras;

        private ZXing.Result[] barCodeResults;
        public ZXing.Result[] BarCodeResults
        {
            get => barCodeResults;
            set
            {
                barCodeResults = value;
                if (barCodeResults != null && barCodeResults.Length > 0)
                {
                    Departure = JsonSerializer.Deserialize<SearchDeparture>(barCodeResults[0].Text, _serializerOptions);
                    BarcodeText = barCodeResults[0].Text;
                }
                else
                    BarcodeText = "No barcode detected";
                OnPropertyChanged(nameof(BarcodeText));
            }
        }
        public string BarcodeText { get; set; } = "No barcode detected";

        [RelayCommand]
        private void SetNumCam()
        {
            if (NumCameras > 0)
                Camera = Cameras.First();
        }
        public RegisterViewModel()
        {
            Title = "Register seeker";
            BarCodeOptions = new Camera.MAUI.ZXingHelper.BarcodeDecodeOptions
            {
                AutoRotate = true,
                PossibleFormats = { ZXing.BarcodeFormat.QR_CODE },
                ReadMultipleCodes = false,
                TryHarder = true,
                TryInverted = true
            };
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

      
    }
}
