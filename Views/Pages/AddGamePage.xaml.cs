using System.Windows.Controls;
using System.Windows.Input;
using gamelib.ViewModels.Pages;

namespace gamelib.Views.Pages;

public partial class AddGamePage : Page
{
    public AddGameViewModel ViewModel { get; }

    public AddGamePage()
    {
        InitializeComponent();
        ViewModel = App.GetRequiredService<AddGameViewModel>();
        DataContext = this;
    }

    private void OnGameItemClick(object sender, MouseButtonEventArgs e)
    {
        ViewModel.OnGameItemClick(sender, e);
    }
}