﻿using AssetTracker.Services;
using AssetTracker.View.Services;
using AssetTracker.ViewModels;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System.Windows;
using System.Windows.Navigation;

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
        private NavigationObserver navObserver;
        private INavigationCoordinator navCoordinator;
        public MainWindow()
        {
            InitializeComponent();
            setServices();
            VM = MainViewModel.Instance;
            navCoordinator.NavigateToLogin();
        }

        public void NavigateToAssetList(object sender, RoutedEventArgs e)
        {
            navCoordinator.NavigateToAssetList();
        }

        public void NavigateToProjectStatus(object sender, RoutedEventArgs e)
        {
            
        }

        private void NavigateToProjectConfig(object sender, RoutedEventArgs e)
        {
            navCoordinator.NavigateToProjectSettings();
        }

        private void NavigateToDashboard(object sender, RoutedEventArgs e)
        {
            navCoordinator.NavigateToUserDashboard();
        }

        private void NavigateToUserEdit(object sender, RoutedEventArgs e)
        {
            navCoordinator.NavigateToUserEdit(VM.CurrentUser);
        }

        private void OnLogoutUser_Clicked(object sender, RoutedEventArgs e)
        {
            VM.LogoutUser();
            navCoordinator.NavigateToLogin();
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfileBtn_Popup.IsOpen = true;
        }

        private void OnProfileSettings_Click(object sender, RoutedEventArgs e)
        {
            navCoordinator.NavigateToUserEdit(VM.CurrentUser);
        }

        private void OnLogout_Click(object sender, RoutedEventArgs e)
        {
            VM.LogoutUser();
            ProfileBtn_Popup.IsOpen = false;
            navCoordinator.NavigateToLogin();
            
        }

        private void setServices()
        {
            
            WindsorContainer container = new WindsorContainer();
            container.Register(Component.For<IViewFactory>()
                                        .ImplementedBy<ViewFactory>());
            container.Register(Component.For<NavigationService>().Instance(ContentFrame.NavigationService));
            container.Register(Component.For<INavigationCoordinator>()
                                        .ImplementedBy<NavigationCoordinator>()
                                        .LifeStyle.Singleton);
            container.Register(Component.For<NavigationObserver>());


            navObserver = container.Resolve<NavigationObserver>();
            navCoordinator = container.Resolve<INavigationCoordinator>();

        }
    }
}
