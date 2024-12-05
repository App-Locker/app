using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppLocker.Views;
using Appwrite;
using Appwrite.Models;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Newtonsoft.Json;

namespace AppLocker;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private Control? _currentView;
    private LoginView? _loginView;

    public Control? CurrentView
    {
        get => _currentView;
        set
        {
            if (_currentView == value) return;
            _currentView = value;
            OnPropertyChanged();
            UpdateSelectionFlags();
        }
    }

    public bool IsHomeSelected { get; private set; }
    public bool IsApplicationsSelected { get; private set; }
    public bool IsActivitySelected { get; private set; }
    public bool IsSettingsSelected { get; private set; }
    
    public ICommand NavigateHomeCommand { get; }
    public ICommand NavigateApplicationsCommand { get; }
    public ICommand NavigateActivityCommand { get; }
    public ICommand NavigateSettingsCommand { get; }
    public ICommand LoginCallCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;
    public MainWindowViewModel()
    {
        NavigateHomeCommand = new RelayCommand(NavigateHome);
        NavigateApplicationsCommand = new RelayCommand(NavigateApplications);
        NavigateActivityCommand = new RelayCommand(NavigateActivity);
        NavigateSettingsCommand = new RelayCommand(NavigateSettings);
        LoginCallCommand = new RelayCommand(LoginCall);
        NavigateHome();
    }

    private async void loadOnlineAppsIntoFile()
    {
        string email = "test@test.test";
        string password = "test1234";
        try
        {
            BackendClient.Instance.CreateUserSession(email, password);
            DocumentList list = await BackendClient.Instance.ReadDataSetAsync(
                "673f05bc0006477e6b18",
                "673f05f800123ce12c32",
                Query.Equal("user_id",BackendClient.Instance.Session.UserId));
            var json = JsonConvert.SerializeObject(list.Documents, Formatting.Indented);
            string filePath = "locked_apps.json";
            string appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                "AppLocker"
            );
            Directory.CreateDirectory(appDataPath); // Ensure the directory exists
            using (StreamWriter writer = new StreamWriter(Path.Combine(appDataPath,filePath)))
            {
                writer.WriteLine(json);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message}: {e.Data}");   
        }

    }

    private void NavigateHome()
    {
        if (IsHomeSelected) return;
        CurrentView = new Lazy<HomeView>(() => new HomeView()).Value;
    }
    public async void LoginCall()
    {
        if (_loginView == null || !_loginView.IsVisible) _loginView = new LoginView();
        var loginContent = new LoginContent
        {
            DataContext = new LoginContentViewModel(ShowRegistration)
        };
        _loginView.Content = loginContent;
        if (!_loginView.IsVisible)
            await _loginView.ShowDialog(
                App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
                    ? desktop.MainWindow
                    : null);
    }
    private void ShowRegistration()
    {
        if (_loginView == null)
            return;
        var registrationContent = new RegisterView
        {
            DataContext = new RegistrationContentViewModel(LoginCall)
        };
        _loginView.Content = registrationContent;
    }

    private void NavigateApplications()
    {
        if (IsApplicationsSelected) return;
        CurrentView = new Lazy<ApplicationsView>(() => new ApplicationsView()).Value;
    }

    private void NavigateActivity()
    {
        if (IsActivitySelected) return;
        CurrentView = new Lazy<ActivitiesView>(() => new ActivitiesView()).Value;
    }

    private void NavigateSettings()
    {
        if (IsSettingsSelected) return;
        CurrentView = new Lazy<SettingsView>(() => new SettingsView()).Value;
    }
    
    private void UpdateSelectionFlags()
    {
        IsHomeSelected = CurrentView is HomeView;
        IsApplicationsSelected = CurrentView is ApplicationsView;
        IsActivitySelected = CurrentView is ActivitiesView;
        IsSettingsSelected = CurrentView is SettingsView;
        OnPropertyChanged(nameof(IsHomeSelected));
        OnPropertyChanged(nameof(IsApplicationsSelected));
        OnPropertyChanged(nameof(IsActivitySelected));
        OnPropertyChanged(nameof(IsSettingsSelected));
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}