using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AppLockerUI;

public class SidebarWidthToOpacityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            // Fully visible if width >= 100, fade out otherwise
            return width >= 650 ? 1.0 : 0.0;
        }

        return 1.0; // Default to fully visible
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

public class InvertSidebarWidthToOpacityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            // Fully visible if width < 100, fade out otherwise
            return width < 650 ? 1.0 : 0.0;
        }

        return 1.0; // Default to fully visible
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}



