using System.Windows;
using System.Windows.Controls;
using gamelib.Services;
using Wpf.Ui.TaskBar;

namespace gamelib.Pages;

public partial class DashboardPage
{
    public DashboardPage()
    {
        InitializeComponent();

        Loaded += MainWindow_Loaded;
    }
    
    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // TODO: remove all of this, was just meant for testing
        var rawgService = new RawgService();
        var game = await rawgService.GetGameAsync(41494);
        
        Console.WriteLine(game.Title);
    }

    private void TaskbarStateComboBox_OnSelectionChanged(
        object sender,
        SelectionChangedEventArgs e
    )
    {
        if (sender is not ComboBox comboBox)
            return;

        var parentWindow = Window.GetWindow(this);

        if (parentWindow == null)
            return;

        var selectedIndex = comboBox.SelectedIndex;

        switch (selectedIndex)
        {
            case 1:
                TaskBarProgress.SetValue(
                    parentWindow,
                    TaskBarProgressState.Normal,
                    80
                );
                break;

            case 2:
                TaskBarProgress.SetValue(
                    parentWindow,
                    TaskBarProgressState.Error,
                    80
                );
                break;

            case 3:
                TaskBarProgress.SetValue(
                    parentWindow,
                    TaskBarProgressState.Paused,
                    80
                );
                break;

            case 4:
                TaskBarProgress.SetValue(
                    parentWindow,
                    TaskBarProgressState.Indeterminate,
                    80
                );
                break;

            default:
                TaskBarProgress.SetState(
                    parentWindow,
                    TaskBarProgressState.None
                );
                break;
        }
    }
}