using System.Globalization;
using System.Windows.Data;

namespace WpfRename.Converters;

/// <summary>
/// null이 아니면 true를 반환하는 컨버터
/// </summary>
public class NullToBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
