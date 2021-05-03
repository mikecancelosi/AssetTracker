using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssetTracker.View.Properties
{
    public class TextPropertyExtension
    {
        public static DependencyProperty TextSourceProperty =
          DependencyProperty.RegisterAttached("TextSource",
                                           typeof(string),
                                           typeof(TextPropertyExtension),
                                           new PropertyMetadata(null));
        public static int GetTextSource(DependencyObject target)
        {
            return (int)target.GetValue(TextSourceProperty);
        }
        public static void SetTextSource(DependencyObject target, string value)
        {
            target.SetValue(TextSourceProperty, value);
        }
    }
}
