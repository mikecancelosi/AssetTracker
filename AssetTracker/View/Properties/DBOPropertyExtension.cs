using System.Windows;

namespace Quipu.View.Properties
{
    /// <summary>
    /// Used to store DBO ids
    /// </summary>
    public class DBOPropertyExtension
    {
        public static DependencyProperty IDSourceProperty =
        DependencyProperty.RegisterAttached("IDSource",
                                            typeof(int),
                                            typeof(DBOPropertyExtension),
                                            new PropertyMetadata(null));
        public static int GetIDSource(DependencyObject target)
        {
            return (int)target.GetValue(IDSourceProperty);
        }
        public static void SetIDSource(DependencyObject target, int value)
        {
            target.SetValue(IDSourceProperty, value);
        }
    }
}
