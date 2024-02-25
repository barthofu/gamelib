using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using gamelib.Helpers;
using gamelib.Models;
using gamelib.Services;

namespace gamelib.ViewModels.Pages;

public class AddGameViewModel : INotifyPropertyChanged
{
    private readonly GameService _gameService;
    private readonly RawgService _rawgService;
    private readonly ToastService _toastService;
    private string _query = string.Empty;

    public ICommand OnGameItemClickCommand => new AsyncRelayCommand(OnGameItemClick);

    public AddGameViewModel(
        RawgService rawgService,
        GameService gameService,
        ToastService toastService)
    {
        _rawgService = rawgService;
        _gameService = gameService;
        _toastService = toastService;
        Search();
    }

    public ObservableCollection<Game> Games { get; } = new();

    public string Query
    {
        get => _query;
        set
        {
            _query = value;
            OnPropertyChanged(nameof(Query));
            Search();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void Search()
    {
        var games = await _rawgService.SearchGamesAsync(Query);

        Games.Clear();
        games.ToList().ForEach(Games.Add);
    }

    public async Task OnGameItemClick(object? sender)
    {
        if (sender is not Game game) return;

        // Check if the game is already in the database
        // if it is, don't add it
        if (await _gameService.GameExists(game)) _toastService.Show(Level.Error, "Error", "Game already exists in the database.");

        // Add the game to the database
        if (await _gameService.AddGame(game)) _toastService.Show(Level.Success, "Game added", "Game added to the database.");
    }
}