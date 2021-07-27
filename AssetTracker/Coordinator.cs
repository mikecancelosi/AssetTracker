using AssetTracker.Model;
using AssetTracker.View;
using AssetTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AssetTracker
{
    public class Coordinator
    {
        private NavigationService nav;
        public delegate void ViewDelegate(Page p);
        public event ViewDelegate OnNavigateSelected;
        private Page queuedPage;
        private bool AnyListeners => (OnNavigateSelected?.GetInvocationList().Count() ?? 0) > 1;

        public Coordinator(NavigationService navigationService)
        {
            nav = navigationService;
        }

        private void NavigateTo(Page page)
        {
            if (!AnyListeners)
            {
                nav.Navigate(page);
                ClearDelegates();
            }
            else
            {
                queuedPage = page;
                OnNavigateSelected?.Invoke(queuedPage);
                ClearDelegates();
            }
        }

        public void NavigateToQueued()
        {
            ClearDelegates();
            NavigateTo(queuedPage);
        }

        private void ClearDelegates()
        {
            if (OnNavigateSelected != null)
            {
                foreach (ViewDelegate d in OnNavigateSelected.GetInvocationList())
                {
                    OnNavigateSelected -= d;
                }
            }
        }

        public void NavigateToLogin()
        {
            LoginViewModel vm = new LoginViewModel();
            Login login = new Login(vm,this);
            NavigateTo(login);
        }

        public void NavigateToCreateUser()
        {
            UserEditViewModel vm = new UserEditViewModel();
            UserEdit userEdit = new UserEdit(vm,this);
            NavigateTo(userEdit);

        }
        public void NavigateToUserEdit(User userToEdit)
        {
            UserEditViewModel vm = new UserEditViewModel(userToEdit);
            UserEdit userEdit = new UserEdit(vm, this);
            NavigateTo(userEdit);

        }

        public void NavigateToAssetDetail(Asset asset)
        {
            AssetDetailViewModel vm = new AssetDetailViewModel();
            vm.myAsset = asset;
            AssetDetail assetDetail = new AssetDetail(vm, this);
            NavigateTo(assetDetail);
        }

        public void NavigateToUserDashboard()
        {
            UserDashboardViewModel vm = new UserDashboardViewModel();
            UserDashboard dashboard = new UserDashboard(vm, this);
            NavigateTo(dashboard);
        }

        public void NavigateToCreateRole()
        {
            RoleEditViewModel vm = new RoleEditViewModel();
            RoleEdit roleEdit = new RoleEdit(vm,this);
            NavigateTo(roleEdit);
        }
        public void NavigateToRoleEdit(SecRole roleToEdit)
        {
            RoleEditViewModel vm = new RoleEditViewModel(roleToEdit);
            RoleEdit roleEdit = new RoleEdit(vm, this);
            NavigateTo(roleEdit);
        }

        public void NavigateToProjectSettings()
        {
            ProjectSettingsViewModel vm = new ProjectSettingsViewModel();
            ProjectSettings projectSettings = new ProjectSettings(vm,this);
            NavigateTo(projectSettings);
        }

        public void NavigateToAssetList()
        {
            AssetListViewModel vm = new AssetListViewModel();
            AssetList assetList = new AssetList(vm,this);
            NavigateTo(assetList);
        }

        public void NavigateToCreateCategory()
        {
            CategoryEditViewModel vm = new CategoryEditViewModel();
            CategoryEdit catEdit = new CategoryEdit(vm,this);
            NavigateTo(catEdit);
        }
        public void NavigatetoCategoryEdit(AssetCategory cat)
        {
            CategoryEditViewModel vm = new CategoryEditViewModel(cat);
            CategoryEdit catEdit = new CategoryEdit(vm, this);
            NavigateTo(catEdit);
        }


    }
}
