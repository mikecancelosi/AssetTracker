using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AssetTracker.View.Converters
{
    public class IsNullToBorderConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                           Type targetType,
                           object parameter,
                           CultureInfo culture)
        {
            if (values[0] != null)
            {
                return new Border();
            }

            string thickStr = values[1] as string;
            ThicknessConverter conv = new ThicknessConverter();
            Thickness thickness = (Thickness)conv.ConvertFromString(thickStr);


            return thickness;
        }

        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
