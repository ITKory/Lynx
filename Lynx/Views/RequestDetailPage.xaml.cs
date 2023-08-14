namespace Lynx.Views;

public partial class RequestDetailPage : ContentPage
{
	public RequestDetailPage(RequestDetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}