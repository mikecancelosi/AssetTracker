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
    /// Interaction logic for SearchboxField.xaml
    /// </summary>
    public partial class SearchboxField : UserControl
    {
        private Type dboType;
        private SearchBoxViewModel vm;

        public SearchboxField()
        {
            InitializeComponent();

        }       

        public void SetSyncObject(DatabaseBackedObject sync)
        {
            dboType = sync.GetType();
            vm = new SearchBoxViewModel(sync);
            DataContext = vm;
            ((SearchBox)searchbox.Child).SetViewModel(vm);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Open Searchbox with initialized type
            searchbox.IsOpen = true;
        }
    }
}
