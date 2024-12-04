using Avalonia.Controls;

namespace AppLockerUI.Views;

public partial class ApplicationsView : UserControl
{
    public ApplicationsView()
    {
        InitializeComponent();
        var viewModel = new ApplicationViewModel();
        DataContext = viewModel;
    }
}