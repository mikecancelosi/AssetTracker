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

        public delegate void NotifyOfChange();
        public event NotifyOfChange OnSelectionChanged;
        private bool SettingFilter = false;

        public Searchbox()
        {
            InitializeComponent();
        }

        public void SetType(Type dboType)
        {
            VM = new SearchBoxViewModel(dboType);
            Results.IsOpen = false;
        }

        public async void SetFilter(string filter)
        {
            if (!SettingFilter)
            {
                SettingFilter = true;
                await VM.SetBaseFilter(filter);
                SettingFilter = false;
            }
        }

        private void ItemClicked(object sender, RoutedEventArgs e)
        {
            DatabaseBackedObject dbo = MainGrid.SelectedItem as DatabaseBackedObject;
            if (dbo != null)
            {
                VM.SelectionChanged(dbo.ID);
                OnSelectionChanged?.Invoke();
                Results.IsOpen = false;
            }
        }       

        public void SetCurrentSelectedObject(int objectID)
        {
            VM.SelectionChanged(objectID);
            OnSelectionChanged?.Invoke();
        }

        public void ClearInvocationList()
        {
            foreach (Delegate d in OnSelectionChanged.GetInvocationList())
            {
                OnSelectionChanged -= (NotifyOfChange)d;
            }
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
                VM.SelectionChanged(dbo.ID);
                OnSelectionChanged?.Invoke();
                Results.IsOpen = false;
            }
        }

        public void ResetDisplay()
        {
            InputText.Text = "";
            VM.SetUserFilter(InputText.Text);
            Results.IsOpen = false;
        }
    }
}
