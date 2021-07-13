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

        public Coordinator(NavigationService navigationService)
        {
            nav = navigationService;
        }

        public void NavigateToCreateUser()
        {
            UserEdit userEdit = new UserEdit(this);
            nav.Navigate(userEdit);
        }
        public void NavigateToModifyUser(User userToEdit)
        {
            UserEdit userEdit = new UserEdit(userToEdit, this);
            nav.Navigate(userEdit);
        }

        public void NavigateToAssetDetail(Asset asset)
        {
            AssetDetail assetDetail = new AssetDetail(asset);
            nav.Navigate(assetDetail);
        }

        public void NavigateToUserDashboard()
        {
            UserDashboard dashboard = new UserDashboard();
            nav.Navigate(dashboard);
        }

        public void NavigateToRoleEdit(SecRole roleToEdit)
        {
            RoleEdit roleEdit = new RoleEdit(roleToEdit);
            nav.Navigate(roleEdit);
        }

        public void NavigateToProjectSettings()
        {
            ProjectSettings projectSettings = new ProjectSettings(this);
            nav.Navigate(projectSettings);
        
        }

        public void NavigateToAssetList()
        {
            AssetList assetList = new AssetList(this);
            nav.Navigate(assetList);
        }

        public void NavigatetoCategoryEdit(AssetCategory cat)
        {
            CategoryEdit catEdit = new CategoryEdit(cat);
            nav.Navigate(catEdit);
        }
    }
}
