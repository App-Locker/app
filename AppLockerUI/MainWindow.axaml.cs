using Avalonia.Controls;

namespace AppLockerUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        MinHeight = 410;
        MinWidth = 200;
    }
}