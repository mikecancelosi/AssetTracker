﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AssetTracker.View.Converters
{
    public class MultiBoolAllConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                          Type targetType,
                          object parameter,
                          System.Globalization.CultureInfo culture)
        {
            foreach (object value in values)
            {
                if(!(value is bool))
                {
                    return false;
                }

                if (!(bool)value)
                {
                    return false;
                }
            }

            return true;
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
