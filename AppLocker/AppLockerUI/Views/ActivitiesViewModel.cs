using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace AppLocker.Views;

public class ActivitiesViewModel
{
    public static ObservableCollection<ItemModel> Items { get; set; } = new();
}
public class ItemModel
{
    private int maxIndex = int.MaxValue;
    public string Text { get; }
    public string ImagePath { get; }
    public int Index { get; }

    public ItemModel(string text, string imagePath)
    {
        Text = text;
        ImagePath = imagePath;
        Index = maxIndex--;
    }
}