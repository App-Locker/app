using Appwrite.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using File = System.IO.File;

namespace AppLocker.Views;

public partial class ApplicationsView : UserControl
{
    private ApplicationViewModel viewModel;

    public ApplicationsView()
    {
        InitializeComponent();
        viewModel = new ApplicationViewModel();
        DataContext = viewModel;
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        string filePath = "locked_apps.json";
        string appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AppLocker"
        );
        Directory.CreateDirectory(appDataPath); // Ensure the directory exists
        using (StreamWriter writer = new StreamWriter(Path.Combine(appDataPath, filePath)))
        {
            //writer.WriteLine(viewModel.Items);
        }
    }

    protected override void OnInitialized()
    {
        string appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AppLocker"
        );
        Directory.CreateDirectory(appDataPath);
        string jsonString = File.ReadAllText(Path.Combine(appDataPath, "locked_apps.json"));
        List<Document> documents = JsonConvert.DeserializeObject<List<Document>>(jsonString);
        documents.ForEach(Console.WriteLine);
    }
}