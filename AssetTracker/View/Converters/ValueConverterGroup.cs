using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace AssetTracker.View.Converters
{
    /// <summary>
    /// Used to allow multiple converters to run through the data to return desired output
    /// </summary>
    public class ValueConverterGroup : List<IValueConverter>, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return this.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}