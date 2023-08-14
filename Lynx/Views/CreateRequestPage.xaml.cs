namespace Lynx.Views;

public partial class CreateRequestPage : ContentPage
{
	public CreateRequestPage(CreateRequestViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}