using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.TaskBar;

namespace gamelib.Pages;

public partial class DashboardPage
{
    public DashboardPage()
    {
        InitializeComponent();
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