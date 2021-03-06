using AssetTracker.ViewModels;
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
    }
}
