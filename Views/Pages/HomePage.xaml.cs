using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using gamelib.Context;
using gamelib.Models;
using gamelib.Services;
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

    private void OnGameItemClick(object sender, MouseButtonEventArgs e)
    {
        ViewModel.OnGameItemClick(sender, e);
    }

}