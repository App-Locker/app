using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia.Media.Imaging;

namespace AppLocker.Views;

public class ApplicationViewModel : INotifyPropertyChanged
{
    private ItemViewModel? _selectedItem;

    public ApplicationViewModel()
    {
        AddCommand = new RelayCommand(() => AddItem(new Bitmap(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"AppLockerUI","Assets", "lockWhite.png")), "VSCODE"));
        RemoveCommand = new RelayCommand(
            () => RemoveItem(SelectedItem),
            () => SelectedItem != null
        );
    }

    public ObservableCollection<ItemViewModel> Items { get; set; } = new();

    public ICommand AddCommand { get; }
    public ICommand RemoveCommand { get; }

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

    public event PropertyChangedEventHandler? PropertyChanged;

    public void AddItem(Bitmap image, string text)
    {
        Items.Add(new ItemViewModel { Image = image, Text = text });
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
    public Bitmap? Image { get; set; }
    public string Text { get; set; } = string.Empty;
}