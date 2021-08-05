using AssetTracker.ViewModels;
using DomainModel;
using System.Windows;
using System.Windows.Controls;

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
