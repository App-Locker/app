using System;
using System.Windows.Input;

namespace AppLockerUI.Views;

public class LoginContentViewModel
{
    public LoginContentViewModel(Action navigateToRegister)
    {
        RegisterCallCommand = new RelayCommand(() => navigateToRegister());
    }

    public ICommand RegisterCallCommand { get; }
}