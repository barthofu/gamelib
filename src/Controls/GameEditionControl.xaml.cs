using System.Windows;
using System.Windows.Controls;

namespace gamelib.Controls;

public partial class GameEditionControl : UserControl
{
    public static readonly DependencyProperty TitleProperty = DependencyProperty
        .Register("Title", typeof(string), typeof(GameEditionControl), new PropertyMetadata(""));

    public static readonly DependencyProperty IsStarredProperty = DependencyProperty
        .Register("IsStarred", typeof(bool), typeof(GameEditionControl), new PropertyMetadata(false));

    public static readonly DependencyProperty DescriptionProperty = DependencyProperty
        .Register("Description", typeof(string), typeof(GameEditionControl), new PropertyMetadata(""));

    public GameEditionControl()
    {
        InitializeComponent();
    }

    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public bool IsStarred
    {
        get => (bool)GetValue(IsStarredProperty);
        set => SetValue(IsStarredProperty, value);
    }

    public string? Description
    {
        get => (string?)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
}