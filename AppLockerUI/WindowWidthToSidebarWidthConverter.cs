using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AppLockerUI;

public class WindowWidthToSidebarWidthConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double windowWidth)
            return windowWidth < 650 ? 45 : 200; // Collapse to 50px if window width < 650, else expand to 200px

        return 200; // Default to expanded
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}