using System.Windows;
using System.Windows.Threading;
using gamelib.Context;
using gamelib.Exceptions;
using gamelib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace gamelib;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static readonly IHost _host = new HostBuilder()
        .ConfigureAppConfiguration(builder =>
        {
            builder
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables();
        })
        .ConfigureServices((_, services) =>
        {
            services.AddDbContext<GamelibDbContext>(
                options =>
                {
                    options
                        .UseSqlite("Data Source=gamelib.db")
                        .UseLazyLoadingProxies();
                }
            );

            services.AddSingleton<RawgService>();

            services.AddSingleton<MainWindow>();
        })
        .Build();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host.Start();

        try
        {
            _host.Services.GetRequiredService<GamelibDbContext>().Database.Migrate();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }

        DispatcherUnhandledException += Application_DispatcherUnhandledException;

        _host.Services.GetRequiredService<MainWindow>().Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        _host.StopAsync().Wait();
        _host.Dispose();
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

    public static T GetRequiredService<T>()
        where T : class
    {
        return _host.Services.GetRequiredService<T>();
    }
}