using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Quipu.View.Converters
{
    /// <summary>
    /// true == visible
    /// false == collapsed
    /// </summary>
    [ValueConversion(typeof(Visibility), typeof(Visibility))]
    public class InverseVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility convertedVal = (Visibility)value;
            if (convertedVal == Visibility.Visible)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility convertedVal = (Visibility)value;
            if (convertedVal == Visibility.Visible)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }
    }
}
