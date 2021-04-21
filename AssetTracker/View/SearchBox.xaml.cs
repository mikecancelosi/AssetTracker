using AssetTracker.Model;
using AssetTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Searchbox.xaml
    /// </summary>
    public partial class Searchbox : UserControl, INotifyPropertyChanged
    {
        private Type dboType;
        private SearchBoxViewModel vm;

        public event PropertyChangedEventHandler PropertyChanged;
        public DatabaseBackedObject CurrentSelection => vm.CurrentlySelectedObject;

        public Searchbox()
        {
            InitializeComponent();
        }       

        public void SetType(Type dboType)
        {
            vm = new SearchBoxViewModel(dboType);
            DataContext = vm;
            vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender,e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Open Searchbox with initialized type
            searchbox.IsOpen = true;
        }

        private void ItemDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            DatabaseBackedObject dbo = MainGrid.SelectedItem as DatabaseBackedObject;

            vm.SelectionChanged(dbo.ID);
            CloseClicked(sender, e);
        }

        private void CloseClicked(object sender, MouseButtonEventArgs e)
        {
            searchbox.IsOpen = false;
        }
    }
}
