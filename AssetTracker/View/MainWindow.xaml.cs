﻿using AssetTracker.Services;
using AssetTracker.View.Services;
using AssetTracker.ViewModels;
using AssetTracker.ViewModels.Services;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataAccessLayer.Services;
using DataAccessLayer.Strategies;
using DomainModel;
using System;
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
            throw new NotImplementedException();
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
            ProfileBtn_Popup.IsOpen = false;
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

            // Foundational services
            container.Register(Component.For(typeof(IRepository<>))
                                        .ImplementedBy(typeof(GenericRepository<>)));

            container.Register(Component.For<IUnitOfWork>()
                                        .ImplementedBy<GenericUnitOfWork>());

            // Factories
            container.Register(Component.For<IViewFactory>()
                                        .ImplementedBy<ViewFactory>());

            container.Register(Component.For<IViewModelFactory>()
                                        .ImplementedBy<ViewModelFactory>());

            container.Register(Component.For<IModelValidatorFactory>()
                                        .ImplementedBy<ModelValidatorFactory>());

            container.Register(Component.For<IUnitOfWorkFactory>()
                                        .ImplementedBy<UnitOfWorkFactory>());

            // Navigation
            container.Register(Component.For<NavigationService>()
                                        .Instance(ContentFrame.NavigationService));

            container.Register(Component.For<INavigationCoordinator>()
                                        .ImplementedBy<NavigationCoordinator>()
                                        .LifeStyle.Singleton);

            container.Register(Component.For<NavigationObserver>());

            // Delete strategies
            container.Register(Component.For<IDeleteStrategy<Asset>>()
                                        .ImplementedBy<AssetDeleteStrategy>());

            container.Register(Component.For<IDeleteStrategy<AssetCategory>>()
                                       .ImplementedBy<CategoryDeleteStrategy>());

            container.Register(Component.For<IDeleteStrategy<Phase>>()
                                       .ImplementedBy<PhaseDeleteStrategy>());

            container.Register(Component.For<IDeleteStrategy<SecRole>>()
                                       .ImplementedBy<SecRoleDeleteStrategy>());

            container.Register(Component.For<IDeleteStrategy<Metadata>>()
                                       .ImplementedBy<TagDeleteStrategy>());

            container.Register(Component.For<IDeleteStrategy<User>>()
                                       .ImplementedBy<UserDeleteStrategy>());

            container.Register(Component.For<IDeleteStrategy<Alert>>()
                                       .ImplementedBy<AlertDeleteStrategy>());

            container.Register(Component.For<IDeleteStrategyFactory>()
                                        .ImplementedBy<DeleteStrategyFactory>());


            // Model Validators
            container.Register(Component.For<IModelValidator<Asset>>()
                                        .ImplementedBy<AssetValidator>());

            container.Register(Component.For<IModelValidator<User>>()
                                        .ImplementedBy<UserValidator>());

            container.Register(Component.For<IModelValidator<SecRole>>()
                                       .ImplementedBy<SecRoleValidator>());

            container.Register(Component.For<IModelValidator<AssetCategory>>()
                                       .ImplementedBy<AssetCategoryValidator>());


            navObserver = container.Resolve<NavigationObserver>();
            navCoordinator = container.Resolve<INavigationCoordinator>();

        }
    }
}
