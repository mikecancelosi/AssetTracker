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
    public partial class Searchbox : UserControl
    {
        private Type dboType;
        private SearchBoxViewModel VM
        {
            get { return (SearchBoxViewModel)DataContext; }
            set { DataContext = value; }
        }
        public DatabaseBackedObject CurrentSelection => VM.CurrentlySelectedObject;

        public delegate void NotifyOfChange();
        public event NotifyOfChange OnSelectionChanged;

        public Searchbox()
        {
            InitializeComponent();
        }

        public void SetType(Type dboType)
        {
            VM = new SearchBoxViewModel(dboType);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Open Searchbox with initialized type
            searchbox.IsOpen = true;
        }

        private void ItemDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            DatabaseBackedObject dbo = MainGrid.SelectedItem as DatabaseBackedObject;

            VM.SelectionChanged(dbo.ID);
            OnSelectionChanged?.Invoke();
            CloseClicked(sender, e);
        }

        private void CloseClicked(object sender, MouseButtonEventArgs e)
        {
            searchbox.IsOpen = false;
        }

        public void SetCurrentSelectedObject(int objectID)
        {
            VM.SelectionChanged(objectID);
            OnSelectionChanged?.Invoke();
        }

        public void SetFilter(string filter)
        {
            VM.Filter = filter;
        }

        public void ClearInvocationList()
        {
            foreach (Delegate d in OnSelectionChanged.GetInvocationList())
            {
                OnSelectionChanged -= (NotifyOfChange)d;
            }
        }
    }
}
