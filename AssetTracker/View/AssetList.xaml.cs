﻿using AssetTracker.Model;
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
    /// Interaction logic for AssetList.xaml
    /// </summary>
    public partial class AssetList : UserControl
    {
        private AssetListViewModel vm;
        public AssetList()
        {
            InitializeComponent();
            vm = new AssetListViewModel();
        }

        public void Load()
        {
            MainGrid.DataContext = vm.Assets;
        }
       
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            AssetCreateControl.Visibility = Visibility.Visible;
            // Make everything else not interactable
        }
    }
}