using AssetTracker.ViewModels;
using AssetTracker.ViewModels.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Strategies;
using DomainModel;
using System;
using System.Linq;

namespace AssetTracker.Services
{
    /// <summary>
    /// Base concrete implementation of INavigationCoordinator.
    /// </summary>
    public class NavigationCoordinator : INavigationCoordinator
    {
        public event Action<ViewModel> UserNavigationAttempt;
        public event Action<ViewModel> VMChanged;

        private ViewModel queuedVM { get; set; }
        public bool WaitingToNavigate => queuedVM != null;

        private ViewModel currentVM;
        public ViewModel CurrentVM
        {
            get => currentVM;
            set
            {
                currentVM = value;
                VMChanged?.Invoke(currentVM);
            }
        }

        private IDeleteStrategy<User> userDeleteStrategy;
        private IDeleteStrategy<Asset> assetDeleteStrategy;
        private IDeleteStrategy<SecRole> roleDeleteStrategy;
        private IDeleteStrategy<AssetCategory> catDeleteStrategy;
        private IDeleteStrategy<Alert> alertDeleteStrategy;
        private IDeleteStrategy<Metadata> tagDeleteStrategy;

        public NavigationCoordinator(IDeleteStrategy<User> userDeleteStrat,
                                     IDeleteStrategy<Asset> assetDeleteStrat,
                                     IDeleteStrategy<SecRole> roleDeleteStrat,
                                     IDeleteStrategy<AssetCategory> catDeleteStrat,
                                     IDeleteStrategy<Alert> alertDeleteStrat,
                                     IDeleteStrategy<Metadata> tagDeleteStrat)
        {
            userDeleteStrategy = userDeleteStrat;
            assetDeleteStrategy = assetDeleteStrat;
            roleDeleteStrategy = roleDeleteStrat;
            catDeleteStrategy = catDeleteStrat;
            alertDeleteStrategy = alertDeleteStrat;
            tagDeleteStrategy = tagDeleteStrat;
        }


        public void RequestNavigationTo(ViewModel vm)
        {
            if (CurrentVM?.GetType().GetInterfaces().Contains(typeof(ISavable)) ?? false)
            {
                ISavable savInst = (ISavable)currentVM;
                if (savInst.IsSavable)
                {
                    queuedVM = vm;
                    UserNavigationAttempt?.Invoke(vm);
                    return;
                }
            }

            CurrentVM = vm;
        }

        public void NavigateToQueued()
        {
            if (queuedVM != null)
            {
                CurrentVM = queuedVM;
                queuedVM = null;
            }
        }



        #region BuildViewmodels
        public void NavigateToLogin()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            LoginViewModel vm = new LoginViewModel(this, uow);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateUser()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            UserEditViewModel vm = new UserEditViewModel(this, uow, userDeleteStrategy);
            RequestNavigationTo(vm);

        }
        public void NavigateToUserEdit(User userToEdit)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            UserEditViewModel vm = new UserEditViewModel(this, uow, userDeleteStrategy);
            vm.SetUser(userToEdit);
            RequestNavigationTo(vm);
        }

        public void NavigateToAssetDetail(Asset asset)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            AssetDetailViewModel vm = new AssetDetailViewModel(this, uow, assetDeleteStrategy, tagDeleteStrategy);
            vm.SetAsset(asset);
            RequestNavigationTo(vm);
        }

        public void NavigateToUserDashboard()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            UserDashboardViewModel vm = new UserDashboardViewModel(this, uow);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateRole()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            RoleEditViewModel vm = new RoleEditViewModel(this, uow, roleDeleteStrategy);
            RequestNavigationTo(vm);
        }
        public void NavigateToRoleEdit(SecRole roleToEdit)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            RoleEditViewModel vm = new RoleEditViewModel(this, uow, roleDeleteStrategy);
            vm.Role = roleToEdit;
            RequestNavigationTo(vm);
        }

        public void NavigateToProjectSettings()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            ProjectSettingsViewModel vm = new ProjectSettingsViewModel(this, uow, MainViewModel.Instance.CurrentUser, roleDeleteStrategy, alertDeleteStrategy, userDeleteStrategy, catDeleteStrategy);
            RequestNavigationTo(vm);
        }

        public void NavigateToAssetList()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            AssetListViewModel vm = new AssetListViewModel(this, uow, MainViewModel.Instance.CurrentUser, assetDeleteStrategy);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateCategory()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            CategoryEditViewModel vm = new CategoryEditViewModel(this, uow, catDeleteStrategy);
            RequestNavigationTo(vm);
        }
        public void NavigatetoCategoryEdit(AssetCategory cat)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            CategoryEditViewModel vm = new CategoryEditViewModel(this, uow, catDeleteStrategy);
            vm.Category = cat;
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateAsset()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            AssetDetailViewModel vm = new AssetDetailViewModel(this, uow, assetDeleteStrategy, tagDeleteStrategy);
            RequestNavigationTo(vm);
        }
        #endregion
    }
}
