using Avalonia;
using System;
using Avalonia.Svg.Skia;

namespace AppLocker;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    { 
        Thread appLockerThread = new Thread(AppLocker.StartWatchers);
        appLockerThread.Start();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    } 

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        GC.KeepAlive(typeof(SvgImageExtension).Assembly);
        GC.KeepAlive(typeof(Avalonia.Svg.Skia.Svg).Assembly);
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }
    // private static async void test()
    // {
    //     BackendClient client = new BackendClient();
    //     Blocked_App app = new Blocked_App("Test", "10");
    //     try
    //     {
    //         var document = await client.CreateDataSetAsync("673f05bc0006477e6b18", "673f05f800123ce12c32", app);
    //         Console.WriteLine("Created document ID: " + document.Id);
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine("Failed to create document: " + ex.Message);
    //     }
    // }
}