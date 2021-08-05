using System;
using System.Windows.Data;

namespace AssetTracker.View.Converters
{
    /// <summary>
    /// If any booleans given are true, returns visible
    /// </summary>
    public class MultiBooleanToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                            Type targetType,
                            object parameter,
                            System.Globalization.CultureInfo culture)
        {
            foreach (object value in values)
            {
                if (value is bool && (bool)value)
                {
                    return System.Windows.Visibility.Visible;
                }
            }

            return System.Windows.Visibility.Hidden;
        }

        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
