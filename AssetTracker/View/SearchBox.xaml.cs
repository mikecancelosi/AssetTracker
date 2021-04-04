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
    /// Interaction logic for SearchBox.xaml
    /// </summary>
    public partial class SearchBox : UserControl
    {
        private SearchBoxViewModel viewModel;
        public event EventHandler OnClose;

        public SearchBox(Type objType, SearchBoxViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            MainGrid.DataContext = viewModel.Items;
        }

        private void ItemDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            // Get ID from item

            // Return info to viewmodel

            // Hide self.

        }

        private void CloseClicked(object sender, MouseButtonEventArgs e)
        {
            OnClose?.Invoke(this, e);
        }
    }
}
