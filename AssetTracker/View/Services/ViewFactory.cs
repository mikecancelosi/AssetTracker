using AssetTracker.Services;
using AssetTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AssetTracker.View.Services
{
    public class ViewFactory : IViewFactory
    {
        public Page BuildView(INavigationCoordinator coord, ViewModel vm)
        {
            switch(vm.GetType().Name)
            {
                case "AssetDetailViewModel":
                    return new AssetDetail((AssetDetailViewModel)vm);
                case "AssetListViewModel":
                    return new AssetList((AssetListViewModel)vm, coord);
                case "CategoryEditViewModel":
                    return new CategoryEdit((CategoryEditViewModel)vm, coord);
                case "LoginViewModel":
                    return new Login((LoginViewModel)vm, coord);
                case "ProjectSettingsViewModel":
                    return new ProjectSettings((ProjectSettingsViewModel)vm, coord);
                case "RoleEditViewModel":
                    return new RoleEdit((RoleEditViewModel)vm, coord);
                case "UserDashboardViewModel":
                    return new UserDashboard((UserDashboardViewModel)vm);
                case "UserEditViewModel":
                    return new UserEdit((UserEditViewModel)vm, coord);
                default:
                    throw new Exception();
            }
        }
    }
}
