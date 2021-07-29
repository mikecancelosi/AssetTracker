using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.ViewModels;
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
    /// Interaction logic for RoleEdit.xaml
    /// </summary>
    public partial class RoleEdit : Page
    {
        public RoleEditViewModel VM
        {
            get { return (RoleEditViewModel)DataContext; }
            set { DataContext = value; }
        }

        public RoleEdit(RoleEditViewModel vm)
        {
            InitializeComponent();
            VM = vm;
        }

        public void NavigateToProjectSettings(object sender, RoutedEventArgs e)
        {
            //coordinator.NavigateToProjectSettings();
        }

        public void OnActivateAllClicked(object sender, RoutedEventArgs e)
        {
            VM.ActivateAllPermissions();
        }

        public void OnDeactivateAllClicked(object sender, RoutedEventArgs e)
        {
            VM.DeactivateAllPermissions();
        }

        private void RoleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox nameTextBox = sender as TextBox;
            VM.OnRoleNameChanged(nameTextBox.Text);
        }
    }
}
