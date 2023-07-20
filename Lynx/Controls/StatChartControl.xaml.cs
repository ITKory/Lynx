using Domain.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Lynx.Models;

namespace Lynx.Controls;

public partial class StatChartControl : ContentView 
{
 

    public StatChartControl()
	{
		InitializeComponent();
		MyChart.Series =   new ISeries[]
  {
    new ColumnSeries<int>
    {
         Values = new [] { 6, 4, 8 },
        MaxBarWidth = 30,
        
    }
};
    }
}