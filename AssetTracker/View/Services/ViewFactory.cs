using AssetTracker.Services;
using AssetTracker.ViewModels;
using AssetTracker.ViewModels.Services;
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
        private IControlViewModelFactory controlFactory;
        public ViewFactory(IControlViewModelFactory conFactory)
        {
            controlFactory = conFactory;
        }

        public Page BuildView(INavigationCoordinator coord, ViewModel vm)
        {
            switch(vm.GetType().Name)
            {
                case "AssetDetailViewModel":
                    return new AssetDetail((AssetDetailViewModel)vm, controlFactory);
                case "AssetListViewModel":
                    return new AssetList((AssetListViewModel)vm);
                case "CategoryEditViewModel":
                    return new CategoryEdit((CategoryEditViewModel)vm);
                case "LoginViewModel":
                    return new Login((LoginViewModel)vm);
                case "ProjectSettingsViewModel":
                    return new ProjectSettings((ProjectSettingsViewModel)vm);
                case "RoleEditViewModel":
                    return new RoleEdit((RoleEditViewModel)vm);
                case "UserDashboardViewModel":
                    return new UserDashboard((UserDashboardViewModel)vm);
                case "UserEditViewModel":
                    return new UserEdit((UserEditViewModel)vm,controlFactory);
                default:
                    throw new Exception();
            }
        }
    }
}
