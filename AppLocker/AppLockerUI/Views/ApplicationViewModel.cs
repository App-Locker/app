using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;

namespace AppLocker.Views;

public class ApplicationViewModel : INotifyPropertyChanged
{
    private ItemViewModel? _selectedItem;
    private ApplicationSettingsView _settingsView = new ();

    public ApplicationViewModel()
    {
        AddCommand = new RelayCommand(() => AddItem(new Bitmap("../../../AppLockerUI/Assets/lockWhite.png"),"VSCODE"));
        RemoveCommand = new RelayCommand(
            () => RemoveItem(SelectedItem),
            () => SelectedItem != null
        );
        SettingsCommand = new RelayCommand(
            () => OpenSettings(SelectedItem),
            () => SelectedItem != null
        );
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
        if(item == null) return;
        ApplicationSettingsData? data = Settings.Find(settings => settings.Id == item.Id);
        _settingsView = new ApplicationSettingsView();
        //TODO: fix that the comboboxes work
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
    
    public void AddItem(Bitmap image, string text)
    {
        Items.Add(new ItemViewModel { Id = Items.Count + 1,Image = image, Text = text });
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