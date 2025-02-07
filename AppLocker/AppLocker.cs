using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace AppLocker;

public class AppLocker
{
    public static readonly Dictionary<string, bool> AppToIsUnlocked = new Dictionary<string, bool>();
    

    public static readonly List<string> LockedApps = new List<string>(){
        "chrome.exe",
        "Spotify.exe",
        "msedge.exe"
    };
    public static readonly List<string> LockedAppsPath = new List<string>()
    {
        "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
        "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe",
        "Spotify.exe"
    };

    public static bool isOnline = false;
    private static readonly Queue<string> QueueAppsToAuthenticate = new Queue<string>();
    private static bool _isWindowsHelloOpen = false;
    
    [DllImport("user32.dll")]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    const uint WM_CLOSE = 0x0010;
    public static void StartWatchers()
    {
        EnsureWatchDogServiceIsRunning();
        string startProcessQuery =
            "SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'";

        // Set up the ManagementEventWatcher with the WMI query
        ManagementEventWatcher startProcessWatcher = new ManagementEventWatcher(new WqlEventQuery(startProcessQuery));

        // Add an event handler for when a new process is created
        startProcessWatcher.EventArrived += ProcessCreated;

        // Start listening for events
        startProcessWatcher.Start();

        // Create a WMI query to listen for process stop events
        string stopProcessquery = "SELECT * FROM Win32_ProcessStopTrace";
        ManagementEventWatcher stopProcessWatcher = new ManagementEventWatcher(new WqlEventQuery(stopProcessquery));

        // Define the event handler for process stop events
        stopProcessWatcher.EventArrived += ProcessStop;
        // Start listening for events
        stopProcessWatcher.Start();
    }

    private static void ProcessStop(object sender, EventArrivedEventArgs e)
    {
        EnsureWatchDogServiceIsRunning();
        string? closedProcessName = e.NewEvent.Properties["ProcessName"].Value.ToString();
        if(!AppToIsUnlocked.ContainsKey(closedProcessName)) return;
        if(!AppToIsUnlocked[closedProcessName]) return;
        var searcher = new ManagementObjectSearcher($"SELECT ProcessId, ParentProcessId FROM Win32_Process WHERE Name = '{closedProcessName}'");
        int count = 0;
        using (ManagementObjectCollection objects = searcher.Get())
        {
            foreach (ManagementObject obj in objects)
            {
                string commandLine = obj["CommandLine"]?.ToString() ?? "";
                if (!commandLine.Contains("--type=system"))
                {
                    int pid = Convert.ToInt32(obj["ProcessId"]);
                    int parentPid = Convert.ToInt32(obj["ParentProcessId"]);
                    Console.WriteLine($"Process ID: {pid}, Parent Process ID: {parentPid}");
                    count++;
                }
            }
        }
        if (count == 0)
        {
            Console.WriteLine($"No {closedProcessName} processes found. It might have been closed.");
            AppToIsUnlocked[closedProcessName] = false;
        }
        else
        {
            Console.WriteLine($"{count} {closedProcessName} processes running...");
        }

    }

    
    private static void ProcessCreated(object sender, EventArrivedEventArgs e)
    {
        // Retrieve the process information from the event
        ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
        string? appName = targetInstance.Properties["Name"].Value.ToString();
        //TODO: USE EXEPATH FOR BACKEND
        string? exePath = targetInstance.Properties["ExecutablePath"].Value.ToString();
        Console.WriteLine(exePath);
        Console.WriteLine(appName);
        // Check if the app is blocked
        if (!LockedApps.Contains(appName)) return;
        
        // Check if the app is unlocked
        if (!AppToIsUnlocked.ContainsKey(appName))
            AppToIsUnlocked.Add(appName, false);
        // Check if the program needs to ask for a password
        if (!AppToIsUnlocked[appName])
        {
            //Kill the process if it is locked
            KillApp(appName,targetInstance["ParentProcessId"]);
            if(!QueueAppsToAuthenticate.Contains(appName))
                QueueAppsToAuthenticate.Enqueue(appName);
            if (!_isWindowsHelloOpen)
            {
                _isWindowsHelloOpen = true;
                AuthenticateNextApp(appName);
            }
        }
    }

    private static void KillApp(string appName,object proccessId)
    {
        int processId = Convert.ToInt32(proccessId);
        Process appProcess = Process.GetProcessById(processId);
        try
        {
            if (!appProcess.HasExited)
            {
                Console.WriteLine($"Killing process: {appName} (PID: {processId})");
                SendMessage(appProcess.MainWindowHandle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                Console.WriteLine($"Process killed: {appName} (PID: {processId})");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to kill process {appName} (PID: {processId}): {ex.Message}");
        }
    }

    private static async void AuthenticateNextApp(string appName)
    {
        while (QueueAppsToAuthenticate.Count > 0)
        {
            string appToAuthenticate = QueueAppsToAuthenticate.Peek();
            bool isAuthenticated = await WindowsHelloIntegration.AuthenticateUserAsync();

            if (isAuthenticated)
            {
                
                string appToStart = QueueAppsToAuthenticate.Dequeue();
                AppToIsUnlocked[appToStart] = true;
                StartApplication(appToStart);
                Logger.Success(appName);
            }
            else
            {
                Console.WriteLine($"Authentication failed for {appToAuthenticate}. The app remains locked.");
                Logger.Fail(appName);
                QueueAppsToAuthenticate.Dequeue();
            }
        }

        _isWindowsHelloOpen = false;
    }

    private static void StartApplication(string app)
    {
        try
        {
            // Start the application after authentication
            Process.Start(GetAppPathFromAppName(app));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to start application {app}: {ex.Message}");
        }
    }

    private static void EnsureWatchDogServiceIsRunning()
    {
        // Check if the Watchdog service is running (Currently not implemented)
        // if (Process.GetProcessesByName("AppLockerWatchdog.exe").Length == 0)
        // {
        //     Process.Start("AppLockerWatchdog.exe");
        // }
    }

    private static string GetAppPathFromAppName(string appName)
    {
        foreach (var s in LockedAppsPath)
        {
            if(s.Split('\\').Last().Equals(appName))
                return s;
        }
        return string.Empty;
    }
}