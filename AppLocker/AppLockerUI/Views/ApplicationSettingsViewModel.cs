using Windows.UI.Xaml.Data;

namespace AppLocker.Views;

public class ApplicationSettingsViewModel : INotifyPropertyChanged
{
    private bool _windowsHelloSwitch;

    public bool WindowsHelloSwitch
    {
        get => _windowsHelloSwitch;
        set
        {
            _windowsHelloSwitch = value;
            OnPropertyChanged(nameof(WindowsHelloSwitch));
        }
    }

    private string _passwordTextBox;

    public string PasswordTextBox
    {
        get => _passwordTextBox;
        set
        {
            _passwordTextBox = value;
            OnPropertyChanged(nameof(PasswordTextBox));
        }
    }

    private bool _limitAttempsSwitch;

    public bool LimitAttempsSwitch
    {
        get => _limitAttempsSwitch;
        set
        {
            _limitAttempsSwitch = value;
            OnPropertyChanged(nameof(LimitAttempsSwitch));
        }
    }

    private int _limitAttempsCount;

    public int LimitAttempsCount
    {
        get => _limitAttempsCount;
        set
        {
            _limitAttempsCount = value;
            OnPropertyChanged(nameof(LimitAttempsCount));
        }
    }

    private string _limitAttempsTimeUnit;

    public string LimitAttempsTimeUnit
    {
        get => _limitAttempsTimeUnit;
        set
        {
            _limitAttempsTimeUnit = value;
            OnPropertyChanged(nameof(LimitAttempsTimeUnit));
        }
    }

    private bool _limitTimeSwitch;

    public bool LimitTimeSwitch
    {
        get => _limitTimeSwitch;
        set
        {
            _limitTimeSwitch = value;
            OnPropertyChanged(nameof(LimitTimeSwitch));
        }
    }

    private int _limitTimeCount;

    public int LimitTimeCount
    {
        get => _limitTimeCount;
        set
        {
            _limitTimeCount = value;
            OnPropertyChanged(nameof(LimitTimeCount));
        }
    }

    private string _limitTimeTimeUnit;

    public string LimitTimeTimeUnit
    {
        get => _limitTimeTimeUnit;
        set
        {
            _limitTimeTimeUnit = value;
            OnPropertyChanged(nameof(LimitTimeTimeUnit));
        }
    }

    private bool _warningsSwitch;

    public bool WarningsSwitch
    {
        get => _warningsSwitch;
        set
        {
            _warningsSwitch = value;
            OnPropertyChanged(nameof(WarningsSwitch));
        }
    }

    private int _warningsAmount;

    public int WarningsAmount
    {
        get => _warningsAmount;
        set
        {
            _warningsAmount = value;
            OnPropertyChanged(nameof(WarningsAmount));
        }
    }

    private int _warningsTimeCount;

    public int WarningsTimeCount
    {
        get => _warningsTimeCount;
        set
        {
            _warningsTimeCount = value;
            OnPropertyChanged(nameof(WarningsTimeCount));
        }
    }

    private string _warningsTimeUnit;

    public string WarningsTimeUnit
    {
        get => _warningsTimeUnit;
        set
        {
            _warningsTimeUnit = value;
            OnPropertyChanged(nameof(WarningsTimeUnit));
        }
    }

    public ApplicationSettingsViewModel(ApplicationSettingsData data)
    {
        //TODO: Fix that comboboxes work
        if (data == null) return;
        WindowsHelloSwitch = data.WindowsHelloSwitch;
        PasswordTextBox = data.PasswordTextBox;
        LimitAttempsSwitch = data.LimitAttempsSwitch;
        LimitAttempsCount = data.LimitAttempsCount;
        LimitAttempsTimeUnit = data.LimitAttempsTimeUnit;
        LimitTimeSwitch = data.LimitTimeSwitch;
        LimitTimeCount = data.LimitTimeCount;
        LimitTimeTimeUnit = data.LimitTimeTimeUnit;
        WarningsSwitch = data.WarningsSwitch;
        WarningsAmount = data.WarningsAmount;
        WarningsTimeCount = data.WarningsTimeCount;
        WarningsTimeUnit = data.WarningsTimeUnit;
    }

    public ApplicationSettingsViewModel()
    {
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}