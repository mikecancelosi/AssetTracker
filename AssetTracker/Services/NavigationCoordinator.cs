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
                    queuedVM = CurrentVM;
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
            LoginViewModel vm = new LoginViewModel();
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateUser()
        {
            UserEditViewModel vm = new UserEditViewModel();
            RequestNavigationTo(vm);

        }
        public void NavigateToUserEdit(User userToEdit)
        {
            UserEditViewModel vm = new UserEditViewModel(userToEdit);
            RequestNavigationTo(vm);
        }

        public void NavigateToAssetDetail(Asset asset)
        {
            AssetDetailViewModel vm = new AssetDetailViewModel(this);
            vm.myAsset = asset;
            RequestNavigationTo(vm);
        }

        public void NavigateToUserDashboard()
        {
            UserDashboardViewModel vm = new UserDashboardViewModel();
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateRole()
        {
            RoleEditViewModel vm = new RoleEditViewModel();
            RequestNavigationTo(vm);
        }
        public void NavigateToRoleEdit(SecRole roleToEdit)
        {
            RoleEditViewModel vm = new RoleEditViewModel(roleToEdit);
            RequestNavigationTo(vm);
        }

        public void NavigateToProjectSettings()
        {
            ProjectSettingsViewModel vm = new ProjectSettingsViewModel();
            RequestNavigationTo(vm);
        }

        public void NavigateToAssetList()
        {
            AssetListViewModel vm = new AssetListViewModel(this);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateCategory()
        {
            CategoryEditViewModel vm = new CategoryEditViewModel();
            RequestNavigationTo(vm);
        }
        public void NavigatetoCategoryEdit(AssetCategory cat)
        {
            CategoryEditViewModel vm = new CategoryEditViewModel();
            RequestNavigationTo(vm);
        }
        #endregion
    }
}
