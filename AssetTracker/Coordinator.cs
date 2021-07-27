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
            Login login = new Login(this);
            NavigateTo(login);
        }

        public void NavigateToCreateUser()
        {
            UserEdit userEdit = new UserEdit(this);
            NavigateTo(userEdit);

        }
        public void NavigateToUserEdit(User userToEdit)
        {
            UserEdit userEdit = new UserEdit(userToEdit, this);
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
            UserDashboard dashboard = new UserDashboard();
            NavigateTo(dashboard);
        }

        public void NavigateToCreateRole()
        {
            RoleEdit roleEdit = new RoleEdit(this);
            NavigateTo(roleEdit);
        }
        public void NavigateToRoleEdit(SecRole roleToEdit)
        {
            RoleEdit roleEdit = new RoleEdit(roleToEdit, this);
            NavigateTo(roleEdit);
        }

        public void NavigateToProjectSettings()
        {
            ProjectSettings projectSettings = new ProjectSettings(this);
            NavigateTo(projectSettings);
        }

        public void NavigateToAssetList()
        {
            AssetList assetList = new AssetList(this);
            NavigateTo(assetList);
        }

        public void NavigateToCreateCategory()
        {
            CategoryEdit catEdit = new CategoryEdit(this);
            NavigateTo(catEdit);
        }
        public void NavigatetoCategoryEdit(AssetCategory cat)
        {
            CategoryEdit catEdit = new CategoryEdit(cat, this);
            NavigateTo(catEdit);
        }


    }
}
