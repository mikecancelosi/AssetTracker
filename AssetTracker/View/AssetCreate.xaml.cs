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
            Value_Category.SetSyncObject(viewmodel.AssetToCreate.AssetCategory);
            Value_ParentID.SetSyncObject(viewmodel.AssetToCreate.Parent);
            Value_AssignedTo.SetSyncObject(viewmodel.AssetToCreate.AssignedToUser);
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
