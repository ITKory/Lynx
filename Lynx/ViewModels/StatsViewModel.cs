using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using Lynx.Models;

namespace Lynx.ViewModels;

public partial class StatsViewModel : BaseViewModel
{
    public SpendingBudget AutoTransport { get; set; }
    public SpendingBudget Entertainment { get; set; }
    public SpendingBudget Security { get; set; }
    public IEnumerable<ISeries> DegreesGaugePieSeries { get; set; }
 
    public StatsViewModel()
    {
       
        AutoTransport = new SpendingBudget()
        {
            Title = "Auto & transport",
            Subtitle = "$375 left to spend",
            Cost = "$254.99",
            TotalCost = "of 400",
            Progress = 0.2,
            ProgressColor = Color.FromRgb(0, 250, 217)
        };

        Entertainment = new SpendingBudget()
        {
            Title = "Entertainment",
            Subtitle = "$375 left to spend",
            Cost = "$50.99",
            TotalCost = "of 600",
            Progress = 0.5,
            ProgressColor = Color.FromRgb(255, 166, 153)
        };

        Security = new SpendingBudget()
        {
            Title = "Security",
            Subtitle = "$375 left to spend",
            Cost = "$5.99",
            TotalCost = "of 600",
            Progress = 0.8,
            ProgressColor = Color.FromRgb(94, 0, 245)
        };

        DegreesGaugePieSeries = new GaugeBuilder()
        .WithInnerRadius(280)
        .WithBackgroundInnerRadius(220)
        .WithLabelsSize(0)
        .WithLabelsPosition(PolarLabelsPosition.ChartCenter)
        .AddValue(20, "gauge value", new SKColor(0, 250, 217), SKColors.White)
        .AddValue(50, "gauge value", new SKColor(255, 121, 102), SKColors.White)
        .AddValue(85, "gauge value", new SKColor(94, 0, 245), SKColors.White)
        .BuildSeries();
    }
    [RelayCommand]
    private async void GoToRequests()
    {
        await Shell.Current.GoToAsync($"{nameof(RequestsPage)}", true);
    }
    [RelayCommand]
    private async void GoToCrews()
    {
        await Shell.Current.GoToAsync($"{nameof(CrewsPage)}", true);
    }
    [RelayCommand]
    private async void GoToDeparture()
    {
        await Shell.Current.GoToAsync($"{nameof(DeparturePage)}", true);
    }
    [RelayCommand]
    private async void GoToRegister()
    {
        await Shell.Current.GoToAsync($"{nameof(RegisterPage)}", true);
    }
}
