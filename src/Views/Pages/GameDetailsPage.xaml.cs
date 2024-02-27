using gamelib.Models;
using gamelib.ViewModels.Pages;
using Wpf.Ui.Common.Interfaces;

namespace gamelib.Views.Pages;

public partial class GameDetailsPage : INavigableView<GameDetailsViewModel>
{
    public GameDetailsPage(Game game)
    {
        ViewModel = App.GetRequiredService<GameDetailsViewModel>();
        DataContext = this;
        ViewModel.LoadGame(game);

        InitializeComponent();
    }

    public GameDetailsViewModel ViewModel { get; }
}