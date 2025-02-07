using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace AppLocker.Views;

public partial class ActivitiesView : UserControl
{
    public ObservableCollection<ItemModel> Items { get; } = new()
    {
        new ItemModel("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "../../../AppLockerUI/Assets/lockWhite.png", 0),
        new ItemModel("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "../../../AppLockerUI/Assets/lockWhite.png", 1),
        new ItemModel("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "../../../AppLockerUI/Assets/lockWhite.png", 2),
        new ItemModel("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "../../../AppLockerUI/Assets/lockWhite.png", 3),
        new ItemModel("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "../../../AppLockerUI/Assets/lockWhite.png", 4),
        new ItemModel("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "../../../AppLockerUI/Assets/lockWhite.png", 5),
        new ItemModel("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "../../../AppLockerUI/Assets/lockWhite.png", 6),
        new ItemModel("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "../../../AppLockerUI/Assets/lockWhite.png", 7),
        new ItemModel("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "../../../AppLockerUI/Assets/lockWhite.png", 8),
        new ItemModel("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "../../../AppLockerUI/Assets/lockWhite.png", 9),
        new ItemModel("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "../../../AppLockerUI/Assets/lockWhite.png", 10)
    };

    public ActivitiesView()
    {
        InitializeComponent();
        DataContext = this;
    }
}

public class ItemModel
{
    public string Text { get; }
    public string ImagePath { get; }
    public int Index { get; }

    public ItemModel(string text, string imagePath, int index)
    {
        Text = text;
        ImagePath = imagePath;
        Index = index;
    }
}