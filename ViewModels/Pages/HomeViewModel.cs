using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using gamelib.Helpers;
using gamelib.Models;
using gamelib.Services;
using gamelib.Views.Pages;
using gamelib.Views.Windows;

namespace gamelib.ViewModels.Pages;

public class HomeViewModel : INotifyPropertyChanged
{
    private readonly GameService _gameService;
    private readonly MainWindow _mainWindow;

    private string _filterQuery = string.Empty;

    public ObservableCollection<Game> FilteredGames { get; } = [];

    public ICommand OnGameItemClickCommand => new RelayCommand(OnGameItemClick);

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

    private async void SearchGames()
    {
        FilteredGames.Clear();
        (await _gameService.SearchGames(FilterQuery)).ForEach(FilteredGames.Add);
    }

    public void RefreshGameList()
    {
        FilterQuery = string.Empty;
        SearchGames();
    }

    private void OnGameItemClick(object? sender)
    {
        if (sender is not Game game) return;

        var gameDetailsPage = new GameDetailsPage(game);
        _mainWindow.NavigationStore.NavigateExternal(gameDetailsPage);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}