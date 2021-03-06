using System;
using System.Windows.Data;

namespace AssetTracker.View.Converters
{
    public class MultiBoolAnyConverter : IMultiValueConverter
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
                    return true;
                }
            }

            return false;
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
