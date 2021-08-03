using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.ViewModels;
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

namespace AssetTracker.View
{
    /// <summary>
    /// Interaction logic for AssetList.xaml
    /// </summary>
    public partial class AssetList : Page
    {
        public AssetListViewModel VM
        {
            get { return (AssetListViewModel)DataContext; }
            set { DataContext = value; }
        }

        public AssetList(AssetListViewModel vm)
        {
            InitializeComponent();
            VM = vm;          
        }     

        private void ListViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Asset ast = MainGrid.SelectedItem as Asset;
            VM.NavigateToAssetDetail(ast);
        }

        private void MainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Asset ast = MainGrid.SelectedItem as Asset;
            VM.ChangeSelectedAsset(ast);
        }

        private void DeleteAsset_Clicked(object sender, RoutedEventArgs e)
        {
            if (MainGrid.SelectedItem != null)
            {
                AssetDeletePrompt.Visibility = Visibility.Visible;
            }
        }

        private void DeleteConfirm_Clicked(object sender, RoutedEventArgs e)
        {
            VM.DeleteCurrentlySelectedAsset();
            AssetDeletePrompt.Visibility = Visibility.Collapsed;
        }

        private void DeleteCancel_Clicked(object sender, RoutedEventArgs e)
        {
            AssetDeletePrompt.Visibility = Visibility.Collapsed;
        }
    }
}
