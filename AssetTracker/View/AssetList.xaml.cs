using AssetTracker.ViewModels;
using DomainModel;
using System.Windows;
using System.Windows.Controls;

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
