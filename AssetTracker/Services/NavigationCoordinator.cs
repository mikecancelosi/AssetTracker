using AssetTracker.Model;
using AssetTracker.View;
using AssetTracker.ViewModels;
using AssetTracker.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Services
{
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
            LoginViewModel vm = new LoginViewModel(this);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateUser()
        {
            UserEditViewModel vm = new UserEditViewModel(this);
            RequestNavigationTo(vm);

        }
        public void NavigateToUserEdit(User userToEdit)
        {
            UserEditViewModel vm = new UserEditViewModel(this);
            vm.SetUser(userToEdit);
            RequestNavigationTo(vm);
        }

        public void NavigateToAssetDetail(Asset asset)
        {
            AssetDetailViewModel vm = new AssetDetailViewModel(this);
            vm.SetAsset(asset);
            RequestNavigationTo(vm);
        }

        public void NavigateToUserDashboard()
        {
            UserDashboardViewModel vm = new UserDashboardViewModel(this);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateRole()
        {
            RoleEditViewModel vm = new RoleEditViewModel(this);
            RequestNavigationTo(vm);
        }
        public void NavigateToRoleEdit(SecRole roleToEdit)
        {
            RoleEditViewModel vm = new RoleEditViewModel(this);
            vm.Role = roleToEdit;
            RequestNavigationTo(vm);
        }

        public void NavigateToProjectSettings()
        {
            ProjectSettingsViewModel vm = new ProjectSettingsViewModel(this);
            RequestNavigationTo(vm);
        }

        public void NavigateToAssetList()
        {
            AssetListViewModel vm = new AssetListViewModel(this);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateCategory()
        {
            CategoryEditViewModel vm = new CategoryEditViewModel(this);
            RequestNavigationTo(vm);
        }
        public void NavigatetoCategoryEdit(AssetCategory cat)
        {
            CategoryEditViewModel vm = new CategoryEditViewModel(this);
            vm.Category = cat;
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateAsset()
        {
            AssetDetailViewModel vm = new AssetDetailViewModel(this);
            RequestNavigationTo(vm);
        }
        #endregion
    }
}
