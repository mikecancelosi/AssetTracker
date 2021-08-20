using Quipu.Services;
using Quipu.ViewModels;
using System;
using System.Windows.Controls;

namespace Quipu.View.Services
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
                    return new UserEdit((UserEditViewModel)vm);
                default:
                    throw new Exception();
            }
        }
    }
}
