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
            Value_Category.SetType(typeof(AssetCategory));
            Value_ParentID.SetType(typeof(Asset));
            Value_AssignedTo.SetType(typeof(User));
        }

        public void OnSaveClicked()
        {
            List<Violation> violations;
            if (viewmodel.CreateAsset(Value_Name.Text,Value_Description.Text,Value_Category.SelectedValue,out violations))
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
