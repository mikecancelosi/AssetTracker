using AssetTracker.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
