using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using gamelib.Models;
using gamelib.Services;
using gamelib.Views.Pages;
using gamelib.Views.Windows;
using Wpf.Ui.Mvvm.Contracts;

namespace gamelib.ViewModels.Pages;

public class HomeViewModel : INotifyPropertyChanged
{
    private readonly GameService _gameService;
    private readonly MainWindow _mainWindow;
    
    private string _filterQuery = string.Empty;
    
    public ObservableCollection<Game> FilteredGames { get; } = [];
    
    public HomeViewModel(GameService gameService, MainWindow mainWindow)
    {
        _gameService = gameService;
        _mainWindow = mainWindow;
        
        // init game list
        SearchGames();
    }
    
    public string FilterQuery
    {
        get => _filterQuery;
        set
        {
            _filterQuery = value;
            OnPropertyChanged(nameof(FilterQuery));
            SearchGames();
        }
    }

    private void SearchGames()
    {
        FilteredGames.Clear();
        _gameService.SearchGames(FilterQuery).ToList().ForEach(FilteredGames.Add);
    }

    public void RefreshGameList()
    {
        FilterQuery = string.Empty;
        SearchGames();
    }

    public void OnGameItemClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Border border) return;
        if (border.DataContext is not Game game) return;
        
        var gameDetailsPage = new GameDetailsPage(game);
        _mainWindow.MainFrame.NavigationService?.Navigate(gameDetailsPage);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}