using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using gamelib.Context;
using gamelib.Controls;
using gamelib.Helpers;
using gamelib.Models;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace gamelib.ViewModels.Pages;

public sealed class MeViewModel : INotifyPropertyChanged
{
    private readonly GamelibDbContext _dbContext;
    private readonly IDialogService _dialogService;

    private string? _firstName;
    private int _gamesCount;
    private string? _lastName;

    public MeViewModel(GamelibDbContext gamelibDbContext,
        IDialogService dialogService)
    {
        _dbContext = gamelibDbContext;
        _dialogService = dialogService;

        LoadDataEvent += (_, _) => LoadData();

        LoadDataEvent.Invoke(this, EventArgs.Empty);
    }

    public string Name => _firstName != null && _lastName != null
        ? $"{_firstName} {_lastName}"
        : "Please configure a name.";

    public string GamesCount => $"You have {_gamesCount} game{(_gamesCount > 0 ? "s" : "")}";

    public ICommand EditCommand => new AsyncRelayCommand(OpenEditModal);

    public string Name => _firstName != null && _lastName != null
        ? $"{_firstName} {_lastName}"
        : "Please configure a name.";

    public string GamesCount => $"You have {_gamesCount} game{(_gamesCount > 0 ? "s" : "")}";
    public event PropertyChangedEventHandler? PropertyChanged;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event EventHandler LoadDataEvent;

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

    private async Task OpenEditModal()
    {
        var rootDialog = _dialogService
            .GetDialogControl();
        var control = new MeEditionControl
        {
            Lastname = _lastName,
            Firstname = _firstName
        };

        rootDialog.ButtonLeftName = "Confirm";
        rootDialog.ButtonRightName = "Cancel";

        rootDialog.Content = control;
        rootDialog.DialogHeight = 230;

        var result = await rootDialog
            .ShowAndWaitAsync("Edit my information", "");

        if (result == IDialogControl.ButtonPressed.Left) await HandleEdition(control.Firstname!, control.Lastname!);

        rootDialog.Hide();
    }

    private async Task HandleEdition(string firstname, string lastname)
    {
        await HandleFirstnameEdition(firstname);
        await HandleLastnameEdition(lastname);
        await _dbContext.SaveChangesAsync();

        LoadDataEvent.Invoke(this, EventArgs.Empty);
    }

    private async Task HandleFirstnameEdition(string? firstname)
    {
        if (string.IsNullOrWhiteSpace(firstname)) return;

        firstname = firstname.Trim();

        var firstnameSetting =
            await _dbContext.Settings.Where(s => s.Name == SettingNameEnum.FirstName).FirstOrDefaultAsync();
        if (firstnameSetting != null)
        {
            firstnameSetting.Value = firstname;
            _dbContext
                .Settings
                .Update(firstnameSetting);
        }
        else
        {
            await _dbContext
                .Settings
                .AddAsync(new Setting
                {
                    Name = SettingNameEnum.FirstName,
                    Value = "test"
                });
        }
    }

    private async Task HandleLastnameEdition(string? lastname)
    {
        if (string.IsNullOrWhiteSpace(lastname)) return;

        lastname = lastname.Trim();

        var lastnameSetting =
            await _dbContext.Settings.Where(s => s.Name == SettingNameEnum.LastName).FirstOrDefaultAsync();
        if (lastnameSetting != null)
        {
            lastnameSetting.Value = lastname;
            _dbContext
                .Settings
                .Update(lastnameSetting);
        }
        else
        {
            await _dbContext
                .Settings
                .AddAsync(new Setting
                {
                    Name = SettingNameEnum.LastName,
                    Value = "TEST"
                });
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}