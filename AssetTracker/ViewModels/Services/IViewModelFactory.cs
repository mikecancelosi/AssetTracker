using AssetTracker.Services;
using DataAccessLayer;
using DataAccessLayer.Strategies;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModels.Services
{
    public interface IViewModelFactory
    {
        IDeleteStrategy<User> userDeleteStrategy { get; }
        IDeleteStrategy<Asset> assetDeleteStrategy { get; }
        IDeleteStrategy<SecRole> roleDeleteStrategy { get; }
        IDeleteStrategy<AssetCategory> catDeleteStrategy { get; }
        IDeleteStrategy<Alert> alertDeleteStrategy { get; }
        IDeleteStrategy<Metadata> tagDeleteStrategy { get; }

        LoginViewModel CreateLoginViewModel(INavigationCoordinator navCoord);
        UserEditViewModel CreateUserEditViewModel(INavigationCoordinator navCoord, User userToEdit = null);
        AssetDetailViewModel CreateAssetDetailViewModel(INavigationCoordinator navCoord, Asset asset = null);
        UserDashboardViewModel CreateUserDashboardViewModel(INavigationCoordinator navCoord);
        RoleEditViewModel CreateRoleEditViewModel(INavigationCoordinator navCoord, SecRole roleToEdit = null);
        ProjectSettingsViewModel CreateProjectSettingsViewModel(INavigationCoordinator navCoord);
        AssetListViewModel CreateAssetListViewModel(INavigationCoordinator navCoord);
        CategoryEditViewModel CreateCategoryEditViewModel(INavigationCoordinator navCoord,AssetCategory cat = null);
    }
}
