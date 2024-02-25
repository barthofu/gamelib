using System.Windows.Controls;
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
}