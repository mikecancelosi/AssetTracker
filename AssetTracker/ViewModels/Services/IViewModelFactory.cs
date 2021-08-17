using AssetTracker.Services;
using DataAccessLayer.Strategies;
using DomainModel;

namespace AssetTracker.ViewModels.Services
{
    public interface IViewModelFactory
    {
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
