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
    /// Interaction logic for AssetDetail.xaml
    /// </summary>
    public partial class AssetDetail : Page
    {
        public AssetDetailViewModel vm;
        public AssetDetail()
        {
            InitializeComponent();
        }

        public AssetDetail(Asset model)
        {
            InitializeComponent();
            vm = new AssetDetailViewModel();
            vm.myAsset = model;
            DataContext = vm;
            Load();
        }

        private void Load()
        {
            // Load Hierarchy

            // Load Images
            
            // Load Discussion
            
            // Load State 

            // Load object history

        }
    }
}
