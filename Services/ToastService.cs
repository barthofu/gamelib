using Wpf.Ui.Mvvm.Contracts;

namespace gamelib.Services;

public enum Level: ushort
{
    Error = 0,
    Success = 1,
    Info = 2
}

public class ToastService
{
    
    private readonly Dictionary<Level, ISnackbarService> _snackbars = new();
    
    public void SetToastLevel(Level level, ISnackbarService snackbarService)
    {
        _snackbars[level] = snackbarService;
    }
    
    public void Show(Level level, string message)
    {
        _snackbars[level].Show(message);
    }
}