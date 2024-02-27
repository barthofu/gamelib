using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;

namespace gamelib.Services;

public enum Level : ushort
{
    Error = 0,
    Success = 1,
    Info = 2
}

public class ToastService(ISnackbarService snackbarService)
{
    public void Show(Level level, string title, string message)
    {
        switch (level)
        {
            case Level.Error:
                ShowError(title, message);
                break;
            case Level.Success:
                ShowSuccess(title, message);
                break;
            case Level.Info:
                ShowInfo(title, message);
                break;
            default:
                ShowInfo(title, message);
                break;
        }
    }

    public void ShowError(string title, string message)
    {
        snackbarService.Show(
            title,
            message,
            SymbolRegular.ErrorCircle24,
            ControlAppearance.Danger
        );
    }

    public void ShowSuccess(string title, string message)
    {
        snackbarService.Show(
            title,
            message,
            SymbolRegular.CheckmarkCircle32,
            ControlAppearance.Success
        );
    }

    public void ShowInfo(string title, string message)
    {
        snackbarService.Show(
            title,
            message,
            SymbolRegular.Info24,
            ControlAppearance.Info
        );
    }
}