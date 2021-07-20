﻿using AssetTracker.Model;
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
        public AssetListViewModel VM
        {
            get { return (AssetListViewModel)DataContext; }
            set { DataContext = value; }
        }
        private Coordinator myCoordinator;
        public AssetList(Coordinator coordinator)
        {
            InitializeComponent();
            VM = new AssetListViewModel();
            this.Unloaded += AssetList_Unloaded;
            myCoordinator = coordinator;
        }

        private void AssetList_Unloaded(object sender, RoutedEventArgs e)
        {
            VM.Dispose();
        }      

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Asset ast = ((ListView)sender).SelectedItem as Asset;
            myCoordinator.NavigateToAssetDetail(ast);
        }

        private void AddAsset_Click(object sender, MouseButtonEventArgs e)
        {
            AssetCreateControl.Visibility = Visibility.Visible;
            // Make everything else not interactable
        }

        private void RemoveAsset_Click(object sender, MouseButtonEventArgs e)
        {           
            // Prompt user to verify deletion
        }
    }
}
