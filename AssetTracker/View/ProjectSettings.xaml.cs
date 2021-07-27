using AssetTracker.Model;
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
    /// Interaction logic for ProjectSettings.xaml
    /// </summary>
    public partial class ProjectSettings : Page
    {
        public ProjectSettingsViewModel VM
        {
            get { return (ProjectSettingsViewModel)DataContext; }
            set { DataContext = value; }
        }
        private Coordinator myCoordinator;

        public ProjectSettings(ProjectSettingsViewModel vm, Coordinator coordinator)
        {
            InitializeComponent();
            VM = vm;
            myCoordinator = coordinator;
        }     

        private void CreateUser_Clicked(object sender, RoutedEventArgs e)
        {
            myCoordinator.NavigateToCreateUser();
        }
       
        private void CreateRole_Clicked(object sender, RoutedEventArgs e)
        {
            myCoordinator.NavigateToCreateRole();
        }

        private void CreateCategory_Clicked(object sender, RoutedEventArgs e)
        {
            myCoordinator.NavigateToCreateCategory();
        }

        private void OnOperationClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            string tag = selectedItem.Tag.ToString();
            switch(selectedItem.DataContext.GetType().BaseType.Name)
            {
                case "User":
                    User selectedUser = selectedItem.DataContext as User;
                    OnUserOperationClicked(selectedUser, tag);
                    break;
                case "AssetCategory":
                    AssetCategory selectedCategory = selectedItem.DataContext as AssetCategory;
                    OnCategoryOperationClicked(selectedCategory, tag);
                    break;
                case "SecRole":
                    SecRole selectedRole = selectedItem.DataContext as SecRole;
                    OnRoleOperationClicked(selectedRole, tag);
                    break;

            }
        }

        private void OnUserOperationClicked(User selectedUser,string tag)
        {
            switch(tag)
            {
                case "copy":
                    User copiedUser = VM.CopyUser(selectedUser.us_id);
                    myCoordinator.NavigateToUserEdit(copiedUser);
                    break;
                case "edit":
                    myCoordinator.NavigateToUserEdit(selectedUser);
                    break;
                case "delete":
                    VM.SetSelectedObject(selectedUser);
                    AssetDeletePrompt.Visibility = Visibility.Visible;
                    break;
            }
                
        }

        private void OnRoleOperationClicked(SecRole selectedRole, string tag)
        {
            switch (tag)
            {
                case "copy":
                    SecRole copiedRole = VM.CopyRole(selectedRole.ro_id);
                    myCoordinator.NavigateToRoleEdit(copiedRole);
                    break;
                case "edit":
                    myCoordinator.NavigateToRoleEdit(selectedRole);
                    break;
                case "delete":
                    VM.SetSelectedObject(selectedRole);
                    AssetDeletePrompt.Visibility = Visibility.Visible;
                    break;
            }

        }

        private void OnCategoryOperationClicked(AssetCategory selectedCategory, string tag)
        {
            switch (tag)
            {
                case "copy":
                    AssetCategory copiedCategory = VM.CopyCategory(selectedCategory.ca_id);
                    myCoordinator.NavigatetoCategoryEdit(copiedCategory);
                    break;
                case "edit":
                    myCoordinator.NavigatetoCategoryEdit(selectedCategory);
                    break;
                case "delete":
                    VM.SetSelectedObject(selectedCategory);
                    AssetDeletePrompt.Visibility = Visibility.Visible;
                    break;
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            VM.Reload();
        }

        #region Delete Item

        private void DeleteConfirm_Clicked(object sender, RoutedEventArgs e)
        {
            VM.DeleteSelectedObject();
            AssetDeletePrompt.Visibility = Visibility.Collapsed;
        }

        private void DeleteCancel_Clicked(object sender, RoutedEventArgs e)
        {
            AssetDeletePrompt.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}
