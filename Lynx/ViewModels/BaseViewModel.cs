namespace Lynx.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    public string title;

    [ObservableProperty]
    public bool isAdmin;
    [ObservableProperty]
    public bool isSeeker;

    public BaseViewModel ()
    {
       var roles =  SecureStorage.Default.GetAsync("roles").GetAwaiter().GetResult();
        if (roles != null)
        {
            if (roles.Contains("admin"))
            {
                IsAdmin = true;
            }
            else if (roles.Contains("seeker"))
            {
                IsSeeker = true;
            }
        }
    }
}
