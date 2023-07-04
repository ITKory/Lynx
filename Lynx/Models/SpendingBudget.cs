using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lynx.Models
{
    public class SpendingBudget
    {
        public string Icon { get; set; }
        
        public string Title { get; set; }

        public string Subtitle { get; set; }
        
        public string Cost { get; set; }
        
        public string TotalCost { get; set; }
        public double Progress { get; set; }
        public Color ProgressColor { get; set; }
    }
}
