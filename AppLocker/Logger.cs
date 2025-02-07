using System.Runtime.InteropServices.JavaScript;
using AppLocker.Views;
using ExCSS;

namespace AppLocker;

public class Logger
{
    public static void writeLogFile()
    {
        string formattedTime = DateTime.Now.ToString("dd-MM-yyyy");
        using (StreamWriter sw = FileHandler.CreateFileStream($"{formattedTime}-log.txt","logs"))
        {
            if(ActivitiesViewModel.Items.Count == 0) return;
            foreach (ItemModel line in ActivitiesViewModel.Items)
            {
                sw.WriteLine(line.Text);
            }
        }
    }
    public static string Success(string app)
    {
        string successText = $"({GetFormattedTime()}) {app}: Successful auth";
        Console.WriteLine(successText);
        ActivitiesViewModel.Items.Add(new ItemModel(successText,"../../../AppLockerUI/Assets/lockWhite.png"));
        return $"{GetFormattedTime()};{app};succ";
    }
    public static string Fail(string app)
    {
        string failText = $"({GetFormattedTime()}) {app}: Failed auth";
        Console.WriteLine(failText);
        ActivitiesViewModel.Items.Add(new ItemModel(failText,"../../../AppLockerUI/Assets/lockWhite.png"));
        return $"{GetFormattedTime()};{app};fail";
    }
    public static string Open(string app)
    {
        string openText = $"({GetFormattedTime()}) {app}: Open";
        Console.WriteLine(openText);
        ActivitiesViewModel.Items.Add(new ItemModel(openText,"../../../AppLockerUI/Assets/lockWhite.png"));
        return $"{GetFormattedTime()};{app};open";
    }

    private static string GetFormattedTime()
    {
        return DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
    }
}