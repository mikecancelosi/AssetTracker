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

        public ProjectSettings(Coordinator coordinator)
        {
            InitializeComponent();
            VM = new ProjectSettingsViewModel();
            myCoordinator = coordinator;
        }     

        private void AddUserClicked(object sender, RoutedEventArgs e)
        {
            myCoordinator.NavigateToCreateUser();
        }
       
        private void AddRoleClicked(object sender, RoutedEventArgs e)
        {
            myCoordinator.NavigateToCreateRole();
        }

        private void AddCategoryClicked(object sender, RoutedEventArgs e)
        {
            myCoordinator.NavigateToCreateCategory();
        }

        private void OnOperationClicked(object sender, MouseButtonEventArgs e)
        {
            Border selectedItem = sender as Border;
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
                    VM.DeleteUser(selectedUser.us_id);
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
                    VM.DeleteRole(selectedRole.ro_id);
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
                    VM.DeleteCategory(selectedCategory.ca_id);
                    break;
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            VM.Reload();
        }
    }
}
