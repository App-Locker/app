using System.Drawing;
using Appwrite;
using Appwrite.Models;
using Avalonia.Controls;
using Avalonia.Interactivity;
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
        //TODO: CHANGE FILEPATH
        // saveIcons("");
    }
    
    private void saveIcons(string filepath)
    {
        int newHeight = 100;
        int newWidth = 100;
        try
        {
            // Load the icon
            using (Icon icon = new Icon(filepath))
            {
                // Convert the icon to a bitmap for resizing
                using (Bitmap originalBitmap = icon.ToBitmap())
                {
                    // Create a new bitmap with the desired size
                    using (Bitmap resizedBitmap = new Bitmap(newWidth, newHeight))
                    {
                        // Draw the original bitmap scaled to the new size
                        using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                        {
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.DrawImage(originalBitmap, 0, 0, newWidth, newHeight);
                        }
                        Icon resizedIcon = Icon.FromHandle(resizedBitmap.GetHicon());
                        //TODO: CHANGE FILENAME
                        BackendClient.Instance.AddFileToBucket("67501d510015f198839c", resizedIcon,"",
                            Permission.Read(BackendClient.Instance.Session.UserId));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error resizing icon: {ex.Message}");
        }
    }

    protected override void OnInitialized()
    {
        string appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AppLocker"
        );
        Directory.CreateDirectory(appDataPath);
        if (File.Exists(Path.Combine(appDataPath, "locked_apps.json")))
        {
            string jsonString = File.ReadAllText(Path.Combine(appDataPath, "locked_apps.json"));
            List<Document> documents = JsonConvert.DeserializeObject<List<Document>>(jsonString);
        }
    }
}