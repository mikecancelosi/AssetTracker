using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View;
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

namespace AssetTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel VM
        {
            get { return (MainViewModel)DataContext; }
            set { DataContext = value; }
        }
        private Coordinator myCoordinator;
        public MainWindow()
        {
            InitializeComponent();
            VM = MainViewModel.Instance;
            myCoordinator = new Coordinator(ContentFrame.NavigationService);
            myCoordinator.NavigateToLogin();       
        }

        public void NavigateToAssetList(object sender, MouseEventArgs e)
        {
            myCoordinator.NavigateToAssetList();
        }

        public void NavigateToProjectStatus(object sender, MouseEventArgs e)
        {
            
        }

        private void NavigateToProjectConfig(object sender, MouseEventArgs e)
        {
            myCoordinator.NavigateToProjectSettings();
        }

        private void NavigateToDashboard(object sender, RoutedEventArgs e)
        {
            myCoordinator.NavigateToUserDashboard();
        }

        private void NavigateToUserEdit(object sender, RoutedEventArgs e)
        {
            myCoordinator.NavigateToUserEdit(VM.CurrentUser);
        }

        private void OnLogoutUser_Clicked(object sender, MouseEventArgs e)
        {
            VM.LogoutUser();
            myCoordinator.NavigateToLogin();
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfileBtn_Popup.IsOpen = true;
        }

        private void OnProfileSettings_MouseUp(object sender, MouseButtonEventArgs e)
        {
            myCoordinator.NavigateToUserEdit(VM.CurrentUser);
        }

        private void OnLogout_MouseUp(object sender, MouseButtonEventArgs e)
        {
            VM.LogoutUser();
            ProfileBtn_Popup.IsOpen = false;
            myCoordinator.NavigateToLogin();
            
        }
    }
}
