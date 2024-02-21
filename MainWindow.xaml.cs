using System.Windows.Input;
using gamelib.Context;

namespace gamelib;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly GamelibDbContext _dbContext;

    public MainWindow(GamelibDbContext gamelibDbContext)
    {
        _dbContext = gamelibDbContext;

        Closed += (_, _) => _dbContext.Dispose();

        InitializeComponent();
    }

    protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
    {
        base.OnMouseDoubleClick(e);
        Console.Write("test");
    }
}