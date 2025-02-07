
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace AppLocker.AppLockerUI.Views;

public class AlternatingBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int index)
        {
            Console.WriteLine(index);
            return index % 2 == 0 ? Brushes.Transparent : new SolidColorBrush(Color.Parse("#3D524C4C"));
        }
        return Brushes.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}