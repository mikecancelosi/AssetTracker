﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssetTracker.View.Properties
{
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
