namespace AppLocker.Views;

public class ApplicationSettingsData
{
    public int Id { get; set; }
    public bool WindowsHelloSwitch { get; set; }
    public string PasswordTextBox { get; set; }
    public bool LimitAttempsSwitch { get; set; }
    public int LimitAttempsCount { get; set; }
    public string LimitAttempsTimeUnit { get; set; }
    public bool LimitTimeSwitch { get; set; }
    public int LimitTimeCount { get; set; }
    public string LimitTimeTimeUnit { get; set; }
    public bool WarningsSwitch { get; set; }
    public int WarningsAmount { get; set; }
    public int WarningsTimeCount { get; set; }
    public string WarningsTimeUnit { get; set; }

    public ApplicationSettingsData(bool windowsHelloSwitch, string passwordTextBox, bool limitAttempsSwitch, int limitAttempsCount, string limitAttempsTimeUnit, bool limitTimeSwitch, int limitTimeCount, string limitTimeTimeUnit, bool warningsSwitch, int warningsAmount, int warningsTimeCount, string warningsTimeUnit)
    {
        WindowsHelloSwitch = windowsHelloSwitch;
        PasswordTextBox = passwordTextBox;
        LimitAttempsSwitch = limitAttempsSwitch;
        LimitAttempsCount = limitAttempsCount;
        LimitAttempsTimeUnit = limitAttempsTimeUnit;
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