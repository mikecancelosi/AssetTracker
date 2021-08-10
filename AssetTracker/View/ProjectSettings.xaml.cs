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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            VM.Reload();
        }
    }
}
