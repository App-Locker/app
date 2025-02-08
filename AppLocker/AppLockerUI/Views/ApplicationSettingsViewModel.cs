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

    private bool _limitAttemptsSwitch;

    public bool LimitAttemptsSwitch
    {
        get => _limitAttemptsSwitch;
        set
        {
            _limitAttemptsSwitch = value;
            OnPropertyChanged(nameof(LimitAttemptsSwitch));
        }
    }

    private int _limitAttemptsCount;

    public int LimitAttemptsCount
    {
        get => _limitAttemptsCount;
        set
        {
            _limitAttemptsCount = value;
            OnPropertyChanged(nameof(LimitAttemptsCount));
        }
    }

    private int _limitAttemptsTimeUnit;

    public int LimitAttemptsTimeUnit
    {
        get => _limitAttemptsTimeUnit;
        set
        {
            _limitAttemptsTimeUnit = value;
            OnPropertyChanged(nameof(LimitAttemptsTimeUnit));
        }
    }

    private bool _limitTimeSwitch;
    public List<string> LimitAttempsTimeUnits { get;set; } = new() { "minute","hour","day"};
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

    private int _limitTimeTimeUnit;

    public int LimitTimeTimeUnit
    {
        get => _limitTimeTimeUnit;
        set
        {
            _limitTimeTimeUnit = value;
            OnPropertyChanged(nameof(LimitTimeTimeUnit));
        }
    }
    public List<string> LimitTimeTimeUnits { get; set; } = new() { "day", "week"};

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

    private int _warningsTimeUnit;

    public int WarningsTimeUnit
    {
        get => _warningsTimeUnit;
        set
        {
            _warningsTimeUnit = value;
            OnPropertyChanged(nameof(WarningsTimeUnit));
        }
    }
    public List<string> WarningsTimeUnits { get;set; } = new() { "seconds", "minutes"};

    public ApplicationSettingsViewModel(ApplicationSettingsData data)
    {
        //TODO: Fix that comboboxes work
        WindowsHelloSwitch = data.WindowsHelloSwitch;
        PasswordTextBox = data.PasswordTextBox;
        
        LimitAttemptsSwitch = data.LimitAttemptsSwitch;
        LimitAttemptsCount = data.LimitAttemptsCount;
        if(LimitAttempsTimeUnits.Contains(data.LimitAttemptsTimeUnit))
            LimitAttemptsTimeUnit = LimitAttempsTimeUnits.IndexOf(data.LimitAttemptsTimeUnit);
        else
            LimitAttemptsTimeUnit = 0;
        
        LimitTimeSwitch = data.LimitTimeSwitch;
        LimitTimeCount = data.LimitTimeCount;
        if(LimitTimeTimeUnits.Contains(data.LimitTimeTimeUnit))
            LimitTimeTimeUnit = LimitTimeTimeUnits.IndexOf(data.LimitTimeTimeUnit);
        else
            LimitTimeTimeUnit = 0;
        
        WarningsSwitch = data.WarningsSwitch;
        WarningsAmount = data.WarningsAmount;
        WarningsTimeCount = data.WarningsTimeCount;
        if(WarningsTimeUnits.Contains(data.WarningsTimeUnit))
            WarningsTimeUnit = WarningsTimeUnits.IndexOf(data.WarningsTimeUnit);
        else
            WarningsTimeUnit = 0;
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