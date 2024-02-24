using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using gamelib.Context;
using gamelib.Models;
using gamelib.Services;

namespace gamelib.Views.Pages;

public partial class HomePage
{
    private readonly GamelibDbContext _dbContext;
    private readonly RawgService _rawgService;

    public HomePage()
    {
        _rawgService = App.GetRequiredService<RawgService>();

        InitializeComponent();

        DataContext = this;
        _dbContext = App.GetRequiredService<GamelibDbContext>();

        var games = _dbContext.Games;

        games.ToList().ForEach(Games.Add);
    }

    public ObservableCollection<Game> Games { get; set; } = new();

    private void OnGameItemClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Border border) return;
        if (border.DataContext is not Game game) return;

        var gameDetailsPage = new GameDetailsPage(game);
        NavigationService?.Navigate(gameDetailsPage);
    }

    // TODO: remove
    private async void PopulateDatabase()
    {
        var searchedGames = await _rawgService.SearchGamesAsync("zelda");

        foreach (var game in searchedGames)
        {
            // save the game in the database
            _dbContext.Add(game);
            await _dbContext.SaveChangesAsync();
        }
    }
}