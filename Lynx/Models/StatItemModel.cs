using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx.Models
{
    public class StatItemModel
    {
       public ISeries[] ChartSeries { get; set; }
       public string Icon { get; set; }
       public string Title { get; set; }
       public bool IsVisible { get; set; }  
    }
}
