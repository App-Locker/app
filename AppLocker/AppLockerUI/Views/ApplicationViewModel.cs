namespace AppLocker.Views;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia.Media.Imaging;

public class ApplicationViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ItemViewModel> Items { get; set; } = new();

    public ICommand AddCommand { get; }
    public ICommand RemoveCommand { get; }

    private ItemViewModel? _selectedItem;
    public ItemViewModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged(nameof(SelectedItem));
            ((RelayCommand)RemoveCommand).RaiseCanExecuteChanged();
        }
    }

    public void AddItem(Bitmap image, string text) => Items.Add(new ItemViewModel { Image = image, Text = text });

    public void RemoveItem(ItemViewModel? item)
    {
        if (item != null) Items.Remove(item);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public ApplicationViewModel()
    {
        AddCommand = new RelayCommand(() => AddItem(new Bitmap("../../../AppLockerUI/Assets/lockWhite.png"), "VSCODE"));
        RemoveCommand = new RelayCommand(
            () => RemoveItem(SelectedItem),
            () => SelectedItem != null
        );
    }
}

public class ItemViewModel
{
    public Bitmap? Image { get; set; }
    public string Text { get; set; } = string.Empty;
}