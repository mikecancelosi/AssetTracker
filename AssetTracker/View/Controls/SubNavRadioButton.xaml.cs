using MaterialIcons;
using System.Windows;
using System.Windows.Controls;

namespace Quipu.View.Controls
{
    /// <summary>
    /// Interaction logic for SubNavRadioButton.xaml
    /// </summary>
    public partial class SubNavRadioButton : RadioButton
    {
        /// <summary>
        /// DP for the icon to display
        /// </summary>
        public static  DependencyProperty IconProperty = DependencyProperty.Register("IconChoice",
                                                                                              typeof(MaterialIconType),
                                                                                              typeof(SubNavRadioButton));

        /// <summary>
        /// DP for the label property to display
        /// </summary>
        public static  DependencyProperty LabelProperty = DependencyProperty.Register("LabelContent",
                                                                                                     typeof(string),
                                                                                                     typeof(SubNavRadioButton));

        public SubNavRadioButton() :base ()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is the icon choice set on the control
        /// </summary>
        public MaterialIconType IconChoice
        {
            get { return (MaterialIconType)base.GetValue(IconProperty); }
            set { base.SetValue(IconProperty, value); }
        }
        
        /// <summary>
        /// This is the label to be dispalyed, set on the control
        /// </summary>
        public string LabelContent
        {
            get { return (string)base.GetValue(LabelProperty); }
            set { base.SetValue(LabelProperty, value); }
        }
    }
}
