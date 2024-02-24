using System.Windows.Controls;
using gamelib.Models;

namespace gamelib.Pages;

public partial class GameDetailsPage : Page
{
    private readonly Game _game;
    
    public GameDetailsPage(Game game)
    {
        InitializeComponent();
        _game = game;
    }
}