using gamelib.ViewModels.Pages;
using gamelib.Views.Windows;

namespace gamelib.Views.Pages;

public partial class HomePage
{
    public HomeViewModel ViewModel { get; }

    public HomePage()
    {
        InitializeComponent();
        ViewModel = App.GetRequiredService<HomeViewModel>();
        DataContext = this;

        App.GetRequiredService<MainWindow>().NavigationStore.Navigated += (_, args) =>
        {
            if (args.CurrentPage.PageType == typeof(HomePage)) ViewModel.RefreshGameList();
        };
    }
}