using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Appwrite;

namespace AppLocker.Views;

public class LoginContentViewModel : INotifyPropertyChanged
{
    private string _emailText;

    public string EmailText
    {
        get => _emailText;
        set
        {
            if (_emailText != value)
            {
                _emailText = value;
                OnPropertyChanged(nameof(EmailText));
            }
        }
    }
    private string _passwordText;

    public string PasswordText
    {
        get => _passwordText;
        set
        {
            if (_passwordText != value)
            {
                _passwordText = value;
                OnPropertyChanged(nameof(PasswordText));
            }
        }
    }
    public LoginContentViewModel(Action navigateToRegister)
    {
        RegisterCallCommand = new RelayCommand(() => navigateToRegister());
        LoginCommand = new RelayCommand(() => Login());
        OAuthCallCommand = new RelayCommand(() => OAuth());
    }

    private async void OAuth()
    {
        try
        {
            string url = await BackendClient.Instance.getOAuthToken();
            Console.WriteLine(url);
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch (AppwriteException e)
        {
            //Console.WriteLine($"{e.Message} : {e.InnerException}");
        }
        
    }

    public async Task Login()
    {
        if (EmailText.Equals("") || PasswordText.Equals(""))
        {
            Console.WriteLine("Please input an email and a password");
            return;
        }
        Console.WriteLine(EmailText);
        Console.WriteLine(PasswordText);
        try
        {
            await BackendClient.Instance.CreateUserSession(EmailText, PasswordText);

            if (!BackendClient.Instance.isLoggedIn)
            {
                Console.WriteLine("Not a valid email or password");
                return;
            }
            Console.WriteLine("User logged in successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            EmailText = "";
            PasswordText = "";
        }
    }
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ICommand RegisterCallCommand { get; }
    public ICommand LoginCommand { get; }
    public ICommand OAuthCallCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;
}