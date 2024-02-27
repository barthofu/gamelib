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
    public MainWindow(IDialogService dialogService,
        ISnackbarService snackbarService)
    {
        InitializeComponent();

        dialogService.SetDialogControl(RootDialog);
        snackbarService.SetSnackbarControl(RootSnackbar);
    }
}