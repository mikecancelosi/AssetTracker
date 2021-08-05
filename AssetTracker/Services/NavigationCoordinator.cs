using AssetTracker.ViewModels;
using AssetTracker.ViewModels.Interfaces;
using DataAccessLayer;
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

        private ViewModel currentVM;
        public ViewModel CurrentVM
        {
            get
            {
                return currentVM;
            }
            set
            {
                currentVM = value;
                VMChanged?.Invoke(currentVM);
            }
        }
        private ViewModel queuedVM { get; set; }

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

        public bool WaitingToNavigate => queuedVM != null;

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
            LoginViewModel vm = new LoginViewModel(this,uow);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateUser()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            UserEditViewModel vm = new UserEditViewModel(this, uow);
            RequestNavigationTo(vm);

        }
        public void NavigateToUserEdit(User userToEdit)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            UserEditViewModel vm = new UserEditViewModel(this, uow);
            vm.SetUser(userToEdit);
            RequestNavigationTo(vm);
        }

        public void NavigateToAssetDetail(Asset asset)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            AssetDetailViewModel vm = new AssetDetailViewModel(this, uow);
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
            RoleEditViewModel vm = new RoleEditViewModel(this, uow);
            RequestNavigationTo(vm);
        }
        public void NavigateToRoleEdit(SecRole roleToEdit)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            RoleEditViewModel vm = new RoleEditViewModel(this, uow);
            vm.Role = roleToEdit;
            RequestNavigationTo(vm);
        }

        public void NavigateToProjectSettings()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            ProjectSettingsViewModel vm = new ProjectSettingsViewModel(this, uow);
            RequestNavigationTo(vm);
        }

        public void NavigateToAssetList()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            AssetListViewModel vm = new AssetListViewModel(this, uow);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateCategory()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            CategoryEditViewModel vm = new CategoryEditViewModel(this, uow);
            RequestNavigationTo(vm);
        }
        public void NavigatetoCategoryEdit(AssetCategory cat)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            CategoryEditViewModel vm = new CategoryEditViewModel(this, uow);
            vm.Category = cat;
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateAsset()
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            AssetDetailViewModel vm = new AssetDetailViewModel(this, uow);
            RequestNavigationTo(vm);
        }
        #endregion
    }
}
