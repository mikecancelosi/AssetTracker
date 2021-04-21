using AssetTracker.Model;
using AssetTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssetTracker.View
{
    /// <summary>
    /// Interaction logic for AssetCreate.xaml
    /// </summary>
    public partial class AssetCreate : UserControl
    {
        private AssetCreateViewModel viewmodel;

        public AssetCreate()
        {
            InitializeComponent();
            viewmodel = new AssetCreateViewModel();
            DataContext = viewmodel;
            Value_Category.SetType(typeof(AssetCategory));
            Value_ParentID.SetType(typeof(Asset));
            Value_AssignedTo.SetType(typeof(User));

            Value_Category.PropertyChanged += (s,e) => { viewmodel.OnCategoryChanged(Value_Category.CurrentSelection); };
            Value_ParentID.PropertyChanged += (s, e) => { viewmodel.OnParentAssetChanged(Value_ParentID.CurrentSelection); };
            Value_AssignedTo.PropertyChanged += (s, e) => { viewmodel.OnUserChanged(Value_AssignedTo.CurrentSelection); };
        }

        public void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            List<Violation> violations;
            if (viewmodel.CreateAsset(out violations))
            {
                this.Visibility = Visibility.Hidden;
                ResetDisplay();
            }
            else
            {
                DisplayError();
            }
        }

        private void ResetDisplay()
        {

        }

        private void DisplayError()
        {

        }
    }
}
