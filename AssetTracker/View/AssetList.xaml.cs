using AssetTracker.Model;
using AssetTracker.ViewModel;
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
        private AssetListViewModel vm;
        public AssetList()
        {
            InitializeComponent();
            vm = new AssetListViewModel();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            AssetCreateControl.Visibility = Visibility.Visible;
            // Make everything else not interactable
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Asset ast = MainGrid.SelectedItem as Asset;
            AssetDetail assetDetail = new AssetDetail(ast);
            NavigationService.Navigate(assetDetail);
        }
    }
}
