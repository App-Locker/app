using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Dialogs;
using Avalonia.Platform.Storage;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using UserControl = Avalonia.Controls.UserControl;

namespace AppLocker.Views;

public class ApplicationViewModel : INotifyPropertyChanged
{
    private ItemViewModel? _selectedItem;
    private ApplicationSettingsView _settingsView = new();
    private UserControl _control = null;

    public ApplicationViewModel(UserControl control)
    {
        AddCommand = new RelayCommand(() => AddItem());
        RemoveCommand = new RelayCommand(
            () => RemoveItem(SelectedItem),
            () => SelectedItem != null
        );
        SettingsCommand = new RelayCommand(
            () => OpenSettings(SelectedItem),
            () => SelectedItem != null
        );
        _control = control;
    }

    public ObservableCollection<ItemViewModel> Items { get; set; } = new();
    public List<ApplicationSettingsData> Settings { get; set; } = new();
    public ICommand AddCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand SettingsCommand { get; }

    public ItemViewModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));
            ((RelayCommand)RemoveCommand).RaiseCanExecuteChanged();
            ((RelayCommand)SettingsCommand).RaiseCanExecuteChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private async void OpenSettings(ItemViewModel? item)
    {
        if (item == null) return;
        ApplicationSettingsData? data = Settings.Find(settings => settings.Id == item.Id);
        _settingsView = new ApplicationSettingsView();
        if (data == null)
        {
            Settings.Add(new ApplicationSettingsData(item.Id));
            _settingsView.DataContext = new ApplicationSettingsViewModel();
        }
        else
        {
            _settingsView.DataContext = new ApplicationSettingsViewModel(data);
        }

        if (!_settingsView.IsVisible)
            await _settingsView.ShowDialog(
                App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
                    ? desktop.MainWindow
                    : null);
    }

    public async void AddItem()
    {
        
        TopLevel level = TopLevel.GetTopLevel(_control);
        if(level == null)
            return;
        
        var files = await level.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Open Executable", AllowMultiple = false,
            FileTypeFilter = new List<FilePickerFileType>() { 
                new FilePickerFileType("Executable")
                {
                    Patterns = new List<string>(){"*.exe"}
                }
            }
        });
        
        if (files == null || files.Count == 0)
        {
            return;
        }
        IStorageFile file = files[0];
        string name = file.Name.Split(".")[0];
        string extension = System.IO.Path.GetExtension(file.Name);

        Icon? icon = ExtractIconFromExe(file.Path.AbsolutePath.Replace("%20"," "));

        if (icon != null)
            Items.Add(new ItemViewModel
                { Id = Items.Count + 1, Image = new Bitmap(ConvertBitmapToStream(icon.ToBitmap())), Text = name });
    }

    private static MemoryStream ConvertBitmapToStream(System.Drawing.Bitmap bitmap)
    {
        // Create a MemoryStream
        MemoryStream memoryStream = new MemoryStream();
        
        // Save the bitmap to the MemoryStream in the desired format (e.g., PNG)
        bitmap.Save(memoryStream,ImageFormat.Png);

        // Reset the position of the MemoryStream to the beginning
        memoryStream.Seek(0, SeekOrigin.Begin);
        
        return memoryStream;
    }
    private static Icon? ExtractIconFromExe(string exePath)
    {
        // Load the EXE file and extract the icon
        using (var iconExtractor = new IconExtractor(exePath))
        {
            return iconExtractor.GetIcon(); 
        }
    }

    public void RemoveItem(ItemViewModel? item)
    {
        if (item != null) Items.Remove(item);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
public class ItemViewModel
{
    public int Id { get; set; }
    public Bitmap? Image { get; set; }
    public string Text { get; set; } = string.Empty;
}
public class IconExtractor : IDisposable
{
    private string _filePath;
    
    public IconExtractor(string filePath)
    {
        _filePath = filePath;
    }

    public Icon? GetIcon()
    {
        // Create a new Icon from the executable file
        return Icon.ExtractAssociatedIcon(_filePath);
    }

    public void Dispose()
    {
        // If you have resources to release, dispose here
    }
}