using AssetTracker.Model;
using AssetTracker.Services;
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

        public ProjectSettings(ProjectSettingsViewModel vm)
        {
            InitializeComponent();
            VM = vm;
        }

        private void OnOperationClicked(object sender, RoutedEventArgs e)
        {
            Button selectedItem = sender as Button;
            string tag = selectedItem.Tag.ToString();            
            ProjectSettingsViewModel.OperationType operation;
            switch(tag)
            {
                case "copy":
                    operation = ProjectSettingsViewModel.OperationType.Copy;
                    break;
                case "edit":
                    operation = ProjectSettingsViewModel.OperationType.Edit;
                    break;
                case "delete":
                default:
                    operation = ProjectSettingsViewModel.OperationType.Delete;
                    break;
            }
            DatabaseBackedObject dbo = selectedItem.DataContext as DatabaseBackedObject;
            VM.CompleteDBOOperation(dbo, operation);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            VM.Reload();
        }
    }
}
