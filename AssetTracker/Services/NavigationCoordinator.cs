using Quipu.ViewModels;
using Quipu.ViewModels.Interfaces;
using Quipu.ViewModels.Services;
using DomainModel;
using System;
using System.Linq;

namespace Quipu.Services
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

        /// <summary>
        /// Factory to build viewmodels for when user requests to navigate away
        /// </summary>
        private IViewModelFactory viewModelFactory;

        public NavigationCoordinator(IViewModelFactory vmFactory)
        {
            viewModelFactory = vmFactory;
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

        public void NavigateToLogin()
        {
            LoginViewModel vm = viewModelFactory.CreateLoginViewModel(this);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateUser()
        {
            UserEditViewModel vm = viewModelFactory.CreateUserEditViewModel(this);
            RequestNavigationTo(vm);

        }
        public void NavigateToUserEdit(User userToEdit)
        {
            UserEditViewModel vm = viewModelFactory.CreateUserEditViewModel(this, userToEdit);
            RequestNavigationTo(vm);
        }

        public void NavigateToAssetDetail(Asset asset)
        {
            AssetDetailViewModel vm = viewModelFactory.CreateAssetDetailViewModel(this, asset);
            RequestNavigationTo(vm);
        }

        public void NavigateToUserDashboard()
        {
            UserDashboardViewModel vm = viewModelFactory.CreateUserDashboardViewModel(this);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateRole()
        {
            RoleEditViewModel vm = viewModelFactory.CreateRoleEditViewModel(this);
            RequestNavigationTo(vm);
        }
        public void NavigateToRoleEdit(SecRole roleToEdit)
        {
            RoleEditViewModel vm = viewModelFactory.CreateRoleEditViewModel(this, roleToEdit);
            RequestNavigationTo(vm);
        }

        public void NavigateToProjectSettings()
        {
            ProjectSettingsViewModel vm = viewModelFactory.CreateProjectSettingsViewModel(this);
            RequestNavigationTo(vm);
        }

        public void NavigateToAssetList()
        {
            AssetListViewModel vm = viewModelFactory.CreateAssetListViewModel(this);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateCategory()
        {
            CategoryEditViewModel vm = viewModelFactory.CreateCategoryEditViewModel(this);
            RequestNavigationTo(vm);
        }
        public void NavigatetoCategoryEdit(AssetCategory cat)
        {
            CategoryEditViewModel vm = viewModelFactory.CreateCategoryEditViewModel(this, cat);
            RequestNavigationTo(vm);
        }

        public void NavigateToCreateAsset()
        {
            AssetDetailViewModel vm = viewModelFactory.CreateAssetDetailViewModel(this);
            RequestNavigationTo(vm);
        }
    }
}
