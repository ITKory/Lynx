using Domain.Models;
using Lynx.Models;
using Lynx.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace Lynx.ViewModels;

public partial class MapViewModel : BaseViewModel
{
    [ObservableProperty]
    HtmlWebViewSource mapSource;
   
    LynxApi _lynxService;
    IConfiguration _config;

    public List<ListItemModel> departures;
    public MapViewModel(IConfiguration configuration,LynxApi lynxApi)
    {
        _lynxService = lynxApi;
        _config = configuration;

   }

    public async Task LoadDataAsync(string path)
    {
     departures =  await  _lynxService.GetDataListAsync<ListItemModel>(path);
    }

    public void LoadMap()
    {
        StringBuilder sb = new();

        string[] locations = departures.Select(d => d.Location).ToArray();

        string html = @$"    <!DOCTYPE html>
            <html xmlns=""http://www.w3.org/1999/xhtml"">
                <head>
                   <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
                    <script src=""https://api-maps.yandex.ru/2.1/?lang=ru_RU&amp;apikey={_config.GetSection("yandexApiKey")}"" type=""text/javascript"">
                    </script>
                                <script type=""text/javascript"">
                                ymaps.ready(init);
                                function init() {{
                                    var myMap = new ymaps.Map(""map"", {{
                                            center: [56.326797, 44.006516],
                                            zoom: 8
                                        }}, {{
                                            searchControlProvider: 'yandex#search'
                                        }}); 
                                    myMap.geoObjects";
        sb.Append(html);

        foreach(var departure in departures)
        {
            if (departure.IsActive)
            {
                sb.AppendFormat(@".add(new ymaps.Placemark([{0}],
            {{
			balloonContent: 'Пропавший: <strong>{1}</strong>  Возраст: <strong>{2}</strong>'
		    }},
            {{
			preset: 'islands#icon',
			iconColor: '#0095b6'
		    }}
            ))", departure.Location,departure.Title, DateTime.Now.Year - departure.BDay.Year);
            }
        }

        sb.Append(@" 
                    }
                </script>
                </head>
                    <body>
                        <div id=""map"" style=""width: 600px; height: 800px""></div> 
                    </body>
                </html>");

        MapSource = new HtmlWebViewSource
        {
            Html = sb.ToString(),
        };
    }


}
