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
        public int SelectedValue 
        { 
            get
            {
                return vm.ID;
            }
        }
        private SearchBox searchBox;
        private SearchBoxViewModel vm;

        public SearchboxField()
        {
            InitializeComponent();
        }

        public void SetType(Type type)
        {
            dboType = type;
            vm = new SearchBoxViewModel(type);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Open Searchbox with initialized type
            if(searchBox == null)
            {
                searchBox = new SearchBox(dboType, vm);
                searchBox.OnClose += SearchBox_OnClose;
            }
            searchBox.Visibility = Visibility.Visible;
        }

        private void SearchBox_OnClose(object sender, EventArgs e)
        {
            Value_Label.Content = vm.Label;
        }
    }
}
