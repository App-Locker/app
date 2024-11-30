using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AppLockerUI;

public class BoolToWidthConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is bool isSelected && isSelected ? 5.0 : 0.0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}