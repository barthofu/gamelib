using System.ComponentModel;
using System.Runtime.CompilerServices;
using gamelib.Context;
using gamelib.Models;
using Microsoft.EntityFrameworkCore;

namespace gamelib.ViewModels.Pages;

public class MeViewModel : INotifyPropertyChanged
{
    private GamelibDbContext _dbContext;

    public string Name => _firstName != null && _lastName != null
        ? $"{_firstName} {_lastName}"
        : "Please configure a name.";

    public string GamesCount => $"You have {_gamesCount} game{(_gamesCount > 0 ? "s" : "")}";

    private string? _firstName;
    private string? _lastName;
    private int _gamesCount;

    public MeViewModel(GamelibDbContext gamelibDbContext)
    {
        _dbContext = gamelibDbContext;
        LoadData();
    }

    private async void LoadData()
    {
        SetField(
            ref _firstName,
            await _dbContext.Settings
                .Where(s => s.Name == SettingNameEnum.FirstName)
                .Select(s => s.Value)
                .FirstOrDefaultAsync(),
            nameof(Name)
        );
        SetField(
            ref _lastName,
            await _dbContext.Settings
                .Where(s => s.Name == SettingNameEnum.LastName)
                .Select(s => s.Value)
                .FirstOrDefaultAsync(),
            nameof(Name)
        );
        SetField(ref _gamesCount, await _dbContext.Games.CountAsync(), nameof(GamesCount));
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
}