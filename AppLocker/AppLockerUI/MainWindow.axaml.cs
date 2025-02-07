
using AppLocker.Views;
using Appwrite;
using Appwrite.Models;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Newtonsoft.Json;
using File = System.IO.File;

namespace AppLocker;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        MinHeight = 410;
        MinWidth = 200;
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        saveSession();
        Logger.writeLogFile();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        loadSession();
        loadOnlineAppsIntoFile();
    }

    private void loadSession()
    {
        Session session = readJSON("session.json");
        if (session == null || !BackendClient.isValidSession(session))
        {
            Console.WriteLine("Is not a valid session");
            return;
        }
        BackendClient.Instance.Session = session;
    }
    private void saveSession()
    {
        if (!BackendClient.Instance.isLoggedIn) return;
        saveJSON(BackendClient.Instance.Session,"session.json");
    }
    private async void loadOnlineAppsIntoFile()
    {
        try
        {
            if(!BackendClient.Instance.isLoggedIn) return;
            DocumentList list = await BackendClient.Instance.ReadDataSetAsync(
                "673f05bc0006477e6b18",
                "673f05f800123ce12c32",
                Query.Equal("user_id",BackendClient.Instance.Session.UserId));
            
            saveJSON(list.Documents,"locked_apps.json");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message}: {e.Data}");   
        }

    }

    private Session? readJSON(string fileName)
    {
        string path = FileHandler.CreateFilePath(fileName);
        if (!File.Exists(path)) return null;
        return JsonConvert.DeserializeObject<Session>(File.ReadAllText(path));
        
    }
    private void saveJSON(object toSave,string fileName)
    {
        var json = JsonConvert.SerializeObject(toSave, Formatting.Indented);
        
        using (StreamWriter writer = FileHandler.CreateFileStream(fileName))
        {
            writer.WriteLine(json);
        }
    }
}