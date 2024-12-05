using System;
using System.Windows.Input;
using AppLocker;

namespace AppLocker.Views;

public class LoginContentViewModel
{
    public LoginContentViewModel(Action navigateToRegister)
    {
        RegisterCallCommand = new RelayCommand(() => navigateToRegister());
    }

    public ICommand RegisterCallCommand { get; }
}