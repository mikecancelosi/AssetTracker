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
        public ProjectSettingsViewModel vm;
        private Coordinator myCoordinator;

        public ProjectSettings(Coordinator coordinator)
        {
            InitializeComponent();
            vm = new ProjectSettingsViewModel();
            DataContext = vm;
            myCoordinator = coordinator;
        }     

        private void AddUserClicked(object sender, RoutedEventArgs e)
        {
            myCoordinator.NavigateToCreateUser();
        }
        private void EditUserClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            User selectedUser = selectedItem.DataContext as User;
            myCoordinator.NavigateToUserEdit(selectedUser);
        }
        private void DeleteUserClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            User selectedUser = selectedItem.DataContext as User;
            vm.DeleteUser(selectedUser.us_id);
        }
        private void CopyUserClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            User selectedUser = selectedItem.DataContext as User;
            User copiedUser = vm.CopyUser(selectedUser.us_id);
            myCoordinator.NavigateToUserEdit(copiedUser);
        }

        private void AddRoleClicked(object sender, RoutedEventArgs e)
        {
            myCoordinator.NavigateToCreateUser();
        }
        private void EditRoleClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            SecRole selectedRole = selectedItem.DataContext as SecRole;
            myCoordinator.NavigateToRoleEdit(selectedRole);
        }
        private void DeleteRoleClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            SecRole selectedRole = selectedItem.DataContext as SecRole;
            vm.DeleteRole(selectedRole.ro_id);
        }
        private void CopyRoleClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            SecRole selectedRole = selectedItem.DataContext as SecRole;
            SecRole copiedRole = vm.CopyRole(selectedRole.ro_id);
            myCoordinator.NavigateToRoleEdit(copiedRole);
        }

        private void AddCategoryClicked(object sender, RoutedEventArgs e)
        {
            myCoordinator.NavigateToCreateUser();
        }
        private void EditCategoryClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            AssetCategory selectedCategory = selectedItem.DataContext as AssetCategory;
            myCoordinator.NavigatetoCategoryEdit(selectedCategory);
        }
        private void DeleteCategoryClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            AssetCategory selectedCat = selectedItem.DataContext as AssetCategory;
            vm.DeleteCategory(selectedCat.ca_id);
        }
        private void CopyCategoryClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            AssetCategory selectedCategory = selectedItem.DataContext as AssetCategory;
            AssetCategory copiedCategory = vm.CopyCategory(selectedCategory.ca_id);
            myCoordinator.NavigatetoCategoryEdit(copiedCategory);
        }
    }
}
