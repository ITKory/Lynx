using Domain.Models;
using Lynx.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.ViewModels
{
    [QueryProperty(nameof(SelectedRequest), "selectedRequest")]
    public partial class RequestDetailViewModel:BaseViewModel
    {
        LynxApi _lynxService;

        [ObservableProperty]
        SearchRequest selectedRequest;
        public RequestDetailViewModel(LynxApi lynxApi)
        {
            Title = "Request Detail";
            _lynxService = lynxApi;
        }


    }
}
