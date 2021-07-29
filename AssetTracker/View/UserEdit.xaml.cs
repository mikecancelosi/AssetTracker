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
    /// Interaction logic for UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Page
    {
        public UserEditViewModel VM
        {
            get { return (UserEditViewModel)DataContext; }
            set { DataContext = value; }
        }
      
        public UserEdit(UserEditViewModel vm)
        {
            InitializeComponent();
            VM = vm;           
            Searchbox_Role.SetType(typeof(SecRole));
            Searchbox_Role.SetCurrentSelectedObject(VM.CurrentUser.us_roid);
        }            

        private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox nameTextBox = sender as TextBox;
            VM.OnFirstNameChanged(nameTextBox.Text);
        }

        private void LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox nameTextBox = sender as TextBox;
            VM.OnLastNameChanged(nameTextBox.Text);
        }

        private void DisplayName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox nameTextBox = sender as TextBox;
            VM.OnDisplayNameChanged(nameTextBox.Text);
        }

        private void  Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox passTextBox = sender as TextBox;
            VM.OnPasswordChanged(passTextBox.Text);
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox emailTextBox = sender as TextBox;
            VM.OnEmailChanged(emailTextBox.Text);
        }

        private void Role_OnSelectionChanged()
        {
            VM.OnRoleChanged(Searchbox_Role.CurrentSelection.ID);
            //TODO: Adjust permissions.
        }

        public void OnActivateAllClicked(object sender, RoutedEventArgs e)
        {
            VM.ActivateAllPermissions();
        }

        public void OnDeactivateAllClicked(object sender, RoutedEventArgs e)
        {
            VM.DeactivateAllPermissions();
        }

        public void NavigateToProjectSettings(object sender, RoutedEventArgs e)
        {
            //coordinator.NavigateToProjectSettings();
        }


        #region Delete User

        private void DeleteUser_Clicked(object sender, RoutedEventArgs e)
        {
            UserDeletePrompt.Visibility = Visibility.Visible;
        }

        private void DeleteConfirm_Clicked(object sender, RoutedEventArgs e)
        {
            VM.DeleteUser();
            //coordinator.NavigateToProjectSettings();
        }

        private void DeleteCancel_Clicked(object sender, RoutedEventArgs e)
        {
            UserDeletePrompt.Visibility = Visibility.Collapsed;
        }
        #endregion


    }
}
