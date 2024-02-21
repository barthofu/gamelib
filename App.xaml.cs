using System.Windows;
using System.Windows.Threading;
using gamelib.Context;
using gamelib.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace gamelib;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            using (var context = new GamelibContext())
            {
                context.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }

        DispatcherUnhandledException += Application_DispatcherUnhandledException;
    }

    private void Application_DispatcherUnhandledException(object sender,
        DispatcherUnhandledExceptionEventArgs e)
    {
        e.Handled = true;
        HandleException(e.Exception);
    }

    private void HandleException(Exception exception)
    {
        if (exception is GamelibException gamelibException)
        {
            MessageBox.Show(
                $"{exception.Message}",
                gamelibException.Caption,
                MessageBoxButton.OK
            );
        }
        else
        {
            MessageBox.Show(
                $"Une erreur vient de se produire : {exception.Message}",
                "Erreur",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );

            Current.Shutdown();
        }
    }
}