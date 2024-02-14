using gamelib.Context;

namespace gamelib;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly GamelibContext _context;

    public MainWindow()
    {
        _context = new GamelibContext();
        _context.Database.EnsureCreated();

        Closed += (_, _) => _context.Dispose();

        InitializeComponent();
    }
}