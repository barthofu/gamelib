using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using gamelib.Context;
using gamelib.Models;
using gamelib.Services;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui.TaskBar;

namespace gamelib.Pages;

public partial class HomePage
{
    private readonly RawgService _rawgService;
    private readonly GamelibDbContext _dbContext;
    
    public ObservableCollection<Game> Games { get; set; } = new();
    
    public HomePage()
    {
        _rawgService = App.GetRequiredService<RawgService>();

        InitializeComponent();
        
        DataContext = this;
        _dbContext = App.GetRequiredService<GamelibDbContext>();
        
        var games = _dbContext.Games;

        games.ToList().ForEach(Games.Add);
    }
    
    private void OnGameItemClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Border border)
            return;
        Console.WriteLine("yes");

        if (border.DataContext is not Game game)
            return;

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