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
        private AssetCreateViewModel VM
        {
            get { return (AssetCreateViewModel)DataContext; }
            set { DataContext = value; }
        }

        public AssetCreate()
        {
            InitializeComponent();
            VM = new AssetCreateViewModel();
            Value_Category.SetType(typeof(AssetCategory));
            Value_ParentID.SetType(typeof(Asset));
            Value_AssignedTo.SetType(typeof(User));

            Value_Category.OnSelectionChanged += () => { VM.OnCategoryChanged(Value_Category.CurrentSelection); };
            Value_ParentID.OnSelectionChanged += () => { VM.OnParentAssetChanged(Value_ParentID.CurrentSelection); };
            Value_AssignedTo.OnSelectionChanged += () => { VM.OnUserChanged(Value_AssignedTo.CurrentSelection); };

        }

        public void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            List<Violation> violations;
            if (VM.CreateAsset(out violations))
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
