using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Appwrite;

namespace AppLocker.Views;

public class RegistrationContentViewModel : INotifyPropertyChanged
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
    }
    private string _passwordConfirmText;
    public string PasswordConfirmText
    {
        get => _passwordConfirmText;
        set
        {
            if (_passwordConfirmText != value)
            {
                _passwordConfirmText = value;
                OnPropertyChanged();
            }
        }
    }
    public RegistrationContentViewModel(Action navigateToLogin)
    {
        LoginCallCommand = new RelayCommand(() => navigateToLogin());
        RegisterCommand = new RelayCommand(() => register());
    }

    private async void register()
    {
        if (EmailText.Equals("") || EmailText == null) return;
        if(PasswordText.Equals("") || PasswordText == null) return;
        if(PasswordConfirmText.Equals("") || PasswordConfirmText == null) return;
        if (!PasswordText.Equals(PasswordConfirmText))
        {
            Console.WriteLine("Password not the same");
            PasswordText = "";
            PasswordConfirmText = "";
            return;
        }
        try
        {
            await BackendClient.CreateUserAsync(EmailText, PasswordText);
        }
        catch (AppwriteException e)
        {
            Console.WriteLine($"{e.Message}");
        }
    }

    public ICommand LoginCallCommand { get; }
    public ICommand RegisterCommand { get; }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}