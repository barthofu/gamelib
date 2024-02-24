using gamelib.Context;
using gamelib.Services;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace gamelib.Views.Windows;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly GamelibDbContext _dbContext;

    public MainWindow(GamelibDbContext gamelibDbContext,
        ToastService toastService,
        IDialogService dialogService)
    {
        _dbContext = gamelibDbContext;

        InitializeComponent();

        var infoSnackbar = new SnackbarService();
        infoSnackbar.SetSnackbarControl(InfoSnackbar);
        toastService.SetToastLevel(Level.Info, infoSnackbar);

        var successSnackbar = new SnackbarService();
        successSnackbar.SetSnackbarControl(SuccessSnackbar);
        toastService.SetToastLevel(Level.Success, successSnackbar);

        var errorSnackbar = new SnackbarService();
        errorSnackbar.SetSnackbarControl(ErrorSnackbar);
        toastService.SetToastLevel(Level.Error, errorSnackbar);

        dialogService.SetDialogControl(RootDialog);
    }
}