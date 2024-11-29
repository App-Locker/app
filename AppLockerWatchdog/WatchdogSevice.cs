using System.Diagnostics;
using System.ServiceProcess;
using System.Management;

public class WatchdogService : ServiceBase
{
    private ManagementEventWatcher processStopWatcher;
    private string targetAppName = "AppLocker.exe";

    protected override void OnStart(string[] args)
    {
        StartProcessWatcher();
        EnsureTargetAppIsRunning();
    }

    protected override void OnStop()
    {
    }

    private void StartProcessWatcher()
    {
        string query = "SELECT * FROM Win32_ProcessStopTrace WHERE ProcessName = '" + targetAppName + "'";
        processStopWatcher = new ManagementEventWatcher(new WqlEventQuery(query));
        processStopWatcher.EventArrived += new EventArrivedEventHandler(OnProcessStopped);
        processStopWatcher.Start();
    }

    private void OnProcessStopped(object sender, EventArrivedEventArgs e)
    {
        EnsureTargetAppIsRunning();
    }

    private void EnsureTargetAppIsRunning()
    {
        if (Process.GetProcessesByName(targetAppName).Length == 0)
        {
            Process.Start(targetAppName);
        }
    }
}