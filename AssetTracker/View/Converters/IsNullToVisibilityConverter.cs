using System;
using System.Globalization;
using System.Windows.Data;

namespace AssetTracker.View.Converters
{

    [ValueConversion(typeof(object), typeof(System.Windows.Visibility))]
    public class IsNullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

