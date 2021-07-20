using AssetTracker.Model;
using AssetTracker.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace AssetTracker
{
    public class Coordinator
    {
        private NavigationService nav;
        public delegate void NullDelegate();
        public event NullDelegate OnNavigateSelected;
        private object queuedPage;
        private bool AnyListeners => OnNavigateSelected.GetInvocationList().Count() > 1;

        public Coordinator(NavigationService navigationService)
        {
            nav = navigationService;
            OnNavigateSelected = delegate { };
        }

        private void NavigateTo(object page)
        {
            if (!AnyListeners)
            {
                nav.Navigate(page);
                OnNavigateSelected = delegate { };
            }
            else
            {
                queuedPage = page;
                OnNavigateSelected?.Invoke();
                OnNavigateSelected = delegate { };
            }
        }

        public void NavigateToQueued()
        {
            OnNavigateSelected = delegate { };
            NavigateTo(queuedPage);
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
            AssetDetail assetDetail = new AssetDetail(asset, this);
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
