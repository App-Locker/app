using AppLocker.Views;
using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace AppLocker;

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