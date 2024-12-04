using System;
using System.Windows.Input;

namespace AppLockerUI.Views;

public class RegistrationContentViewModel
{
    public RegistrationContentViewModel(Action navigateToLogin)
    {
        LoginCallCommand = new RelayCommand(() => navigateToLogin());
    }

    public ICommand LoginCallCommand { get; }
}