using gamelib.ViewModels.Pages;
using Wpf.Ui.Common.Interfaces;

namespace gamelib.Pages;

public partial class MePage : INavigableView<MeViewModel>
{
    public MePage()
    {
        ViewModel = App.GetRequiredService<MeViewModel>();
        DataContext = this;

        InitializeComponent();
    }

    public MeViewModel ViewModel { get; }
}