﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AssetTracker.View.Converters
{
    public class DatetimeToTimeElapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
         System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(string))
            {
                throw new InvalidOperationException("The target must be a string");
            }
            if (value.GetType() != typeof(DateTime))
            {
                throw new InvalidOperationException("The input must be a datetime");
            }


            TimeSpan timeSince = DateTime.Now - (DateTime)value;
            if (timeSince.Days > 365)
            {
                return ((DateTime)value).ToString("Y", culture);
            }
            else if (timeSince.Days > 1)
            {
                return ((DateTime)value).ToString("m", culture);
            }
            else if (timeSince.Days == 1)
            {
                return "Yesterday";
            }
            else if (timeSince.Hours > 0)
            {
                return timeSince.Hours + "h";
            }
            else if (timeSince.Minutes > 1)
            {
                return timeSince.Minutes + "m";
            }
            else if(timeSince.Seconds > 30)
            {
                return "<1m";
            }
            else
            {
                return "Just now";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
