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

        private void AddCategoryClicked(object sender, RoutedEventArgs e)
        {
            AddCategoryControl.Visibility = Visibility.Visible;
            CategoryEditorHeaderText.Text = "Create Category";
            vm.OnNewCategoryClicked();
            DataContext = vm;
        }

        private void AddPhaseClicked(object sender, RoutedEventArgs e)
        {
            vm.OnNewPhaseClicked();
        }

        private void PhaseMoveUpClicked(object sender, RoutedEventArgs e)
        {
            int index = (int)((Button)sender).CommandParameter;
            vm.OnPhaseUpClicked(index);
        }

        private void PhaseMoveDownClicked(object sender, RoutedEventArgs e)
        {
            int index = (int)((Button)sender).CommandParameter;
            vm.OnPhaseDownClicked(index);
        }

        private void DeletePhaseClicked(object sender, RoutedEventArgs e)
        {
            int index = (int)((Button)sender).CommandParameter;
            vm.OnPhaseDelete(index);
        }
        private void SaveCategoryClicked(object sender, RoutedEventArgs e)
        {
            vm.OnSaveCategory();
            AddCategoryControl.Visibility = Visibility.Collapsed;
        }

        private void ExitCategoryClicked(object sender, RoutedEventArgs e)
        {
            AddCategoryControl.Visibility = Visibility.Collapsed;
            vm.OnExitCategory();
        }

        private void CategoryListItem_Clicked(object sender, MouseButtonEventArgs e)
        {
            ListViewItem selectedItem = sender as ListViewItem;
            AssetCategory selectedCategory = selectedItem.DataContext as AssetCategory;
            AddCategoryControl.Visibility = Visibility.Visible;
            CategoryEditorHeaderText.Text = "Edit Category";
            vm.OnCategorySelectedForEdit(selectedCategory.ca_id);
        }

        private void EditUserClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            User selectedUser = selectedItem.DataContext as User;
            myCoordinator.NavigateToUserEdit(selectedUser);
        }

        private void EditRoleClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            SecRole selectedRole = selectedItem.DataContext as SecRole;
            myCoordinator.NavigateToRoleEdit(selectedRole);
        }
    }
}
