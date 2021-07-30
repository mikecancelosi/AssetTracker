using AssetTracker.Model;
using AssetTracker.ViewModels;
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
        private SearchBoxViewModel VM
        {
            get { return (SearchBoxViewModel)DataContext; }
            set { DataContext = value; }
        }
        public DatabaseBackedObject CurrentSelection => VM.CurrentlySelectedObject;

        public Searchbox()
        {
            InitializeComponent();
        }

        public void SetViewmodel(SearchBoxViewModel vm)
        {
            VM = vm;
            Results.IsOpen = false;
        }     

        private void SetCurrentSelectedObject(int objectID)
        {
            VM.SetSelectedItem(objectID);
        }       

        private void InputText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsLoaded)
            {
                Results.IsOpen = true;
                VM.SetUserFilter(InputText.Text);
            }
        }       

        private void InputText_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsLoaded)
            {
                Results.IsOpen = true;
            }
        }

        private void InputText_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsLoaded)
            {               
                VM.CheckFilter();
                if (Results.IsOpen)
                {
                    Results.IsOpen = false;
                }
            }
        }

        private void MainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatabaseBackedObject dbo = MainGrid.SelectedItem as DatabaseBackedObject;
            if (dbo != null)
            {
                VM.SetSelectedItem(dbo.ID);
                Results.IsOpen = false;
            }
            else
            {
                SetCurrentSelectedObject(0);
            }
        }
    }
}
