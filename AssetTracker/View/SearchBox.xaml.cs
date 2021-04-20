using AssetTracker.Model;
using AssetTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        public SearchBoxViewModel viewModel;

        public SearchBox()
        {
            InitializeComponent();
        }

        public void SetViewModel(SearchBoxViewModel vm)
        {
            viewModel = vm;
            MainGrid.DataContext = viewModel;
        }

        private void ItemDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            DatabaseBackedObject dbo = MainGrid.SelectedItem as DatabaseBackedObject;

            viewModel.SelectionChanged(dbo.ID);
            CloseClicked(sender, e);
        }

        private void CloseClicked(object sender, MouseButtonEventArgs e)
        {
            ((Popup)this.Parent).IsOpen = false;
        }
    }
}
