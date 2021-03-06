using System;
using System.Globalization;
using System.Windows.Data;

namespace AssetTracker.View.Converters
{
    /// <summary>
    /// true == visible
    /// false == collapsed
    /// </summary>
    [ValueConversion(typeof(bool), typeof(System.Windows.Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {     
            return (bool)value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (System.Windows.Visibility)value == System.Windows.Visibility.Visible;
        }
    }
}
