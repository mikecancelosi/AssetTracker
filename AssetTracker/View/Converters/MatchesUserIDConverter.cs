using Quipu.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Quipu.View.Converters
{
    [ValueConversion(typeof(int), typeof(System.Windows.Visibility))]
    public class MatchesUserIDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return (int)value == MainViewModel.Instance.CurrentUser.ID;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
