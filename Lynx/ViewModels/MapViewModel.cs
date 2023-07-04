namespace Lynx.ViewModels;

public partial class MapViewModel : BaseViewModel
{
    [ObservableProperty]
    HtmlWebViewSource mapSource;
    public MapViewModel()
    {
        MapSource = new HtmlWebViewSource
        {
            Html = @"
            <!DOCTYPE html>
            <html xmlns=""http://www.w3.org/1999/xhtml"">
                <head>
                    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
                    <script src=""https://api-maps.yandex.ru/2.1/?apikey=51a6baee-d33f-4527-ba5b-d396e225de9e&lang=ru_RU"" type=""text/javascript"">
                    </script>
                            <script type=""text/javascript"">
                                ymaps.ready(init);
                                function init(){
                                    var myMap = new ymaps.Map(""map"", {
                                        center: [56.286163, 43.909252],
                                        zoom: 10
                                    });}
                            </script>
                        </head>
                <body>
                    <div id=""map"" style=""width: 600px; height: 600px""></div>
                </body>
                </html>
"
        };

     
    }
}
