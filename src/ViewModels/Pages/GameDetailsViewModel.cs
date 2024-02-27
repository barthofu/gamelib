using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using gamelib.Context;
using gamelib.Controls;
using gamelib.Helpers;
using gamelib.Models;
using gamelib.Services;
using gamelib.Views.Pages;
using gamelib.Views.Windows;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace gamelib.ViewModels.Pages;

public class GameDetailsViewModel : INotifyPropertyChanged
{
    private readonly GamelibDbContext _dbContext;
    private readonly IDialogService _dialogService;
    private readonly NavigationStore _navigationStore;
    private readonly ToastService _toastService;

    private Game? _game { get; set; }

    public string? Title => _game?.Title;
    public string? ReleaseDate => _game?.ReleaseDate;
    public string? CoverImage => _game?.CoverImage;
    public string? Description => _game?.Description;
    public double Rating => _game?.Rating is not null ? double.Round(_game.Rating) : 0d;
    public bool IsStarred => _game?.IsStarred ?? false;
    public string? Tags => _game?.Tags is not null ? string.Join(", ", _game.Tags.Select(t => t.Name)) : null;

    public string? Platforms =>
        _game?.Platforms is not null ? string.Join(", ", _game.Platforms.Select(p => p.Name)) : null;

    public string? Genres => _game?.Genres is not null ? string.Join(", ", _game.Genres.Select(g => g.Name)) : null;
    public string? Url => _game?.Slug is not null ? RawgService.GetRawgUrl(_game) : null;

    public ICommand EditCommand => new AsyncRelayCommand(OpenEditModal);
    public ICommand DeleteCommand => new AsyncRelayCommand(HandleDelete);

    public GameDetailsViewModel(
        GamelibDbContext dbContext,
        IDialogService dialogService,
        MainWindow mainWindow,
        ToastService toastService
    )
    {
        _dbContext = dbContext;
        _dialogService = dialogService;
        _navigationStore = mainWindow.NavigationStore;
        _toastService = toastService;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public void LoadGame(Game game)
    {
        _game = game;
        OnPropertyChanged(null);
    }

    private async Task OpenEditModal(object? _)
    {
        if (_game is null)
        {
            return;
        }

        var rootDialog = _dialogService
            .GetDialogControl();
        var control = new GameEditionControl()
        {
            Title = _game.Title,
            IsStarred = _game.IsStarred,
            Description = _game.Description
        };

        rootDialog.ButtonLeftName = "Confirm";
        rootDialog.ButtonRightName = "Cancel";

        rootDialog.Content = control;
        rootDialog.DialogHeight = 350;

        var result = await rootDialog
            .ShowAndWaitAsync("Edit a game", "");

        if (result == IDialogControl.ButtonPressed.Left) await HandleEdition(control);

        rootDialog.Hide();
    }

    private async Task HandleEdition(GameEditionControl control)
    {
        if (_game is null)
        {
            return;
        }

        _game.Title = control.Title!;
        _game.IsStarred = control.IsStarred;
        _game.Description = control.Description!;

        await _dbContext.SaveChangesAsync();
        OnPropertyChanged(null);
    }

    private async Task HandleDelete(object? _)
    {
        if (_game is not null)
        {
            _dbContext.Games.Remove(_game);
            await _dbContext.SaveChangesAsync();
        }

        _toastService.ShowSuccess("Game deleted", "Game deleted from the database");
        _navigationStore.Navigate(typeof(HomePage));
        _game = null;
    }
}