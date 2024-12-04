using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AppLockerUI;

public class SidebarWidthToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width) return width >= 650; // Visible if width is 100 or more, otherwise hidden

        return false; // Default to hidden
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException(); // ConvertBack is not needed
    }
}

public class SidebarWidthToIsVisibleConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width && parameter is string target)
            return (target == "Text" && width >= 650) || (target == "Image" && width < 650);
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

public class InvertSidebarWidthToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width) return width < 650; // Visible if width is less than 100, otherwise hidden

        return false; // Default to hidden
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException(); // ConvertBack is not needed
    }
}