
using Domain.Models;
using Lynx.Models;
using Lynx.Service;
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

    [ObservableProperty]
    HtmlWebViewSource mapSource;

    LynxApi _lynxService;

    [ObservableProperty]
    List<ParticipantModel> participants;
    public DepartureDetailViewModel()
    {
        _lynxService = new LynxApi();
    }

    [RelayCommand]
    private async void LoadParticipants()
    {
      Participants = await _lynxService.GetParticipantsAsync($"api/departure/participants?departureId={SelectedDeparture.Id}");
    }
    
  

    [RelayCommand]
    private void LoadMap()
    {
        Title = $"{SelectedDeparture.SearchRequest.Lost.Name} detail";
        MapSource = new HtmlWebViewSource
        {
            Html = @$"
            <!DOCTYPE html>
            <html xmlns=""http://www.w3.org/1999/xhtml"">
                <head>
                    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
                    <script src=""https://api-maps.yandex.ru/2.1/?apikey=51a6baee-d33f-4527-ba5b-d396e225de9e&lang=ru_RU"" type=""text/javascript"">
                    </script>
                            <script type=""text/javascript"">
                                ymaps.ready(init);
                                function init(){{
                                    var myMap = new ymaps.Map(""map"", {{
                                        center: [{SelectedDeparture.Location.Latitude}, {SelectedDeparture.Location.Longitude}],
                                        zoom: 13
                                    }});}}
                            </script>
                        </head>
                <body>
                    <div id=""map"" style=""width: 600px; height: 600px""></div>
                </body>
                </html> "
        };

    }

    [RelayCommand]
    private async void JoinDeparture()
    {

        string userId = await SecureStorage.Default.GetAsync("user_id");

        string barCodeJsonContent = JsonSerializer.Serialize(new  SeekerRegistration{ SearchDepartureId = SelectedDeparture.Id , UserId =Convert.ToInt32(userId), StartAt = TimeOnly.FromDateTime(DateTime.Now)});
        await Shell.Current.GoToAsync(nameof(QRCodePage), true, new Dictionary<string, object>
            {
                { "selectedDeparture", barCodeJsonContent }
            });
    }
}
