using gamelib.ViewModels.Pages;

namespace gamelib.Views.Pages;

public partial class HomePage
{
    public HomeViewModel ViewModel { get; }

    public HomePage()
    {
        InitializeComponent();
        ViewModel = App.GetRequiredService<HomeViewModel>();
        DataContext = this;

        if (NavigationService != null)
        {
            NavigationService.Navigated += (_, args) =>
            {
                if (args.Content is HomePage) ViewModel.RefreshGameList();
            };
        }
    }
}