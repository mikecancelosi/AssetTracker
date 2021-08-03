using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AssetTracker.View.Converters
{
    /// <summary>
    /// This converter returns thicknesses depending on if the inital value is null
    /// [valuetocheck,thickness if null, thickness if not null]
    /// </summary>
    public class IsNullToThicknessConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                            Type targetType,
                            object parameter,
                            CultureInfo culture)
        {
            ThicknessConverter conv = new ThicknessConverter();
            string thickStr;
            Thickness thickness;
            if (values[0] != null)
            {
                if (values.Length >= 3)
                {
                    thickStr = values[2] as string;
                    if (conv.IsValid(thickStr))
                    {
                        thickness = (Thickness)conv.ConvertFromString(thickStr);
                        return thickness;
                    }
                }
            }
            else
            {
                thickStr = values[1] as string;
                if (conv.IsValid(thickStr))
                {
                    thickness = (Thickness)conv.ConvertFromString(thickStr);
                    return thickness;
                }
            }

            return new Thickness(0);
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
