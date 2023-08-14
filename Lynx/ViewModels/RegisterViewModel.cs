using Camera.MAUI;
using Camera.MAUI.ZXingHelper;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Domain.Models;
using Lynx.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lynx.ViewModels
{
    public partial class RegisterViewModel:BaseViewModel
    {
        JsonSerializerOptions _serializerOptions;
        LynxApi _lynxService;

        [ObservableProperty]
        BarcodeDecodeOptions barCodeOptions;
        [ObservableProperty]
        SeekerRegistration seeker;

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
                    Seeker = JsonSerializer.Deserialize<SeekerRegistration>(barCodeResults[0].Text);
                    BarcodeText = barCodeResults[0].Text;   
                    try
                    {
                        _lynxService.RegistrationSeekerAsync("api/departure/registration", Seeker.UserId, Seeker.SearchDepartureId,Seeker.StartAt);
                   
                    }catch(Exception  ex)
                    {
                       Shell.Current.DisplayAlert("Error:" ,ex.Message,"Ok");
                    }
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
       
        public async void Detected()
        {
           await Toast.Make("Success registration seeker").Show();

           await Shell.Current.GoToAsync(nameof(StatsPage));
        }
        public RegisterViewModel()
        {
            Title = "Register seeker";
            _lynxService = new LynxApi();
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
