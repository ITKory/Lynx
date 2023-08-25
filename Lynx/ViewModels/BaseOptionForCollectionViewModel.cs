using Lynx.Models;
using Lynx.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.ViewModels
{
    public partial  class BaseOptionForCollectionViewModel:BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<ListItemModel> activeItems = new  ();

        [ObservableProperty]
        ObservableCollection<ListItemModel> disableItems = new  ();

        [ObservableProperty]
        bool isRefresh;
        [ObservableProperty]
        List<ListItemModel> itemsList;

        

        protected LynxApi lynxService;

        public BaseOptionForCollectionViewModel(LynxApi lynxApi)
        {
            lynxService = lynxApi;
        }

        
        public async Task LoadDataAsync(string path)
        {
            IsRefresh = true;
            ItemsList =  await lynxService.GetDataListAsync<ListItemModel>(path);
            if (ActiveItems.Count > 0)
                ActiveItems.Clear();

            if (DisableItems.Count > 0)
                DisableItems.Clear();

            foreach (var item in ItemsList)
            {
                if (item.IsActive)
                {

                    item.ItemColor = Color.FromArgb("#ffebd3");
                    ActiveItems.Add(item);
                }
                else
                {
                    if (item.IsFound)
                    {
                        if (item.IsDied)
                        {
                            item.ItemColor = Color.FromArgb("#ffbfbf");
                        }
                        else
                        {
                            item.ItemColor = Color.FromArgb("#d3f2db");

                        }
                    }
                    else
                    {
                        item.ItemColor = Color.FromArgb("#ffebd3");
                    }
                    DisableItems.Add(item);
                }
            }
            IsRefresh = false;
          

        }


    }
}
