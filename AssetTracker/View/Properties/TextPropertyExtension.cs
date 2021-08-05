using System.Windows;

namespace AssetTracker.View.Properties
{
    /// <summary>
    /// Used to store textsources
    /// </summary>
    public class TextPropertyExtension
    {
        public static DependencyProperty TextSourceProperty =
          DependencyProperty.RegisterAttached("TextSource",
                                           typeof(string),
                                           typeof(TextPropertyExtension),
                                           new PropertyMetadata(null));
        public static string GetTextSource(DependencyObject target)
        {
            return (string)target.GetValue(TextSourceProperty);
        }
        public static void SetTextSource(DependencyObject target, string value)
        {
            target.SetValue(TextSourceProperty, value);
        }
    }
}
