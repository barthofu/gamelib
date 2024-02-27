using System.Windows;
using System.Windows.Controls;

namespace gamelib.Controls;

public partial class MeEditionControl : UserControl
{
    public static readonly DependencyProperty FirstnameProperty = DependencyProperty
        .Register("Firstname", typeof(string), typeof(MeEditionControl), new PropertyMetadata(""));

    public static readonly DependencyProperty LastnameProperty = DependencyProperty
        .Register("Lastname", typeof(string), typeof(MeEditionControl), new PropertyMetadata(""));

    public MeEditionControl()
    {
        InitializeComponent();
    }

    public string? Firstname
    {
        get => (string?)GetValue(FirstnameProperty);
        set => SetValue(FirstnameProperty, value);
    }

    public string? Lastname
    {
        get => (string?)GetValue(LastnameProperty);
        set => SetValue(LastnameProperty, value);
    }
}