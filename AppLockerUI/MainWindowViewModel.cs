using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppLockerUI.Views;
using Avalonia.Controls;

namespace AppLockerUI;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private Control? _currentView;

   public Control? CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }
    
    
    public ICommand NavigateHomeCommand { get; }
    public ICommand NavigateApplicationsCommand { get; }

    public MainWindowViewModel()
    {
        NavigateHomeCommand = new RelayCommand(NavigateHome);
        NavigateApplicationsCommand = new RelayCommand(NavigateApplications);
        NavigateHome();
    }

    private void NavigateHome()
    {
        CurrentView = new HomeView();
    }

    private void NavigateApplications()
    {
        CurrentView = new ApplicationsView();
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}