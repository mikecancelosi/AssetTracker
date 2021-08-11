using MaterialIcons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssetTracker.View.Controls
{
    /// <summary>
    /// Interaction logic for SubNavRadioButton.xaml
    /// </summary>
    public partial class SubNavRadioButton : RadioButton
    {

        public static  DependencyProperty IconProperty = DependencyProperty.Register("IconChoice",
                                                                                              typeof(MaterialIconType),
                                                                                              typeof(SubNavRadioButton));

        public static  DependencyProperty LabelProperty = DependencyProperty.Register("LabelContent",
                                                                                                     typeof(string),
                                                                                                     typeof(SubNavRadioButton));

        public SubNavRadioButton() :base ()
        {
            InitializeComponent();
        }

        public MaterialIconType IconChoice
        {
            get { return (MaterialIconType)base.GetValue(IconProperty); }
            set { base.SetValue(IconProperty, value); }
        }

        public string LabelContent
        {
            get { return (string)base.GetValue(LabelProperty); }
            set { base.SetValue(LabelProperty, value); }
        }
    }
}
