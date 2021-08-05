using System;
using System.Globalization;
using System.Windows.Data;

namespace AssetTracker.View.Converters
{
    /// <summary>
    /// Determines if the value is null or not. 
    /// Handles certain non-nullable classes by using the default values rather than null
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class IsNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return true;
            }

            if(value.GetType() == typeof(string))
            {
                return (string)value == "";
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
