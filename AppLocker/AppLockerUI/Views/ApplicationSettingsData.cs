namespace AppLocker.Views;

public class ApplicationSettingsData
{
    public int Id { get; set; }
    public bool WindowsHelloSwitch { get; set; }
    public string PasswordTextBox { get; set; }
    public bool LimitAttemptsSwitch { get; set; }
    public int LimitAttemptsCount { get; set; }
    public string LimitAttemptsTimeUnit { get; set; }
    public bool LimitTimeSwitch { get; set; }
    public int LimitTimeCount { get; set; }
    public string LimitTimeTimeUnit { get; set; }
    public bool WarningsSwitch { get; set; }
    public int WarningsAmount { get; set; }
    public int WarningsTimeCount { get; set; }
    public string WarningsTimeUnit { get; set; }

    public ApplicationSettingsData(bool windowsHelloSwitch, string passwordTextBox, bool limitAttemptsSwitch, int limitAttemptsCount, string limitAttemptsTimeUnit, bool limitTimeSwitch, int limitTimeCount, string limitTimeTimeUnit, bool warningsSwitch, int warningsAmount, int warningsTimeCount, string warningsTimeUnit)
    {
        WindowsHelloSwitch = windowsHelloSwitch;
        PasswordTextBox = passwordTextBox;
        LimitAttemptsSwitch = limitAttemptsSwitch;
        LimitAttemptsCount = limitAttemptsCount;
        LimitAttemptsTimeUnit = limitAttemptsTimeUnit;
        LimitTimeSwitch = limitTimeSwitch;
        LimitTimeCount = limitTimeCount;
        LimitTimeTimeUnit = limitTimeTimeUnit;
        WarningsSwitch = warningsSwitch;
        WarningsAmount = warningsAmount;
        WarningsTimeCount = warningsTimeCount;
        WarningsTimeUnit = warningsTimeUnit;
    }

    public ApplicationSettingsData(int id)
    {
        Id = id;
    }
}