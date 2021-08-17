using AssetTracker.Services;
using DataAccessLayer;
using DataAccessLayer.Services;
using DataAccessLayer.Strategies;
using DomainModel;

namespace AssetTracker.ViewModels.Services
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IDeleteStrategyFactory deleteStrategyFactory;
        private readonly IUnitOfWorkFactory uowFactory;
        private readonly IModelValidatorFactory modelValidatorFactory;

        public ViewModelFactory(IDeleteStrategyFactory deleteStrategyFactory,
                                IModelValidatorFactory modelValidatorFactory,
                                IUnitOfWorkFactory uowFactory)
        {
            this.deleteStrategyFactory = deleteStrategyFactory;
            this.modelValidatorFactory = modelValidatorFactory;
            this.uowFactory = uowFactory;
        }

        public LoginViewModel CreateLoginViewModel(INavigationCoordinator navCoord)
        {
            IUnitOfWork uow = uowFactory.BuildUOW();
            LoginViewModel vm = new LoginViewModel(navCoord, uow);
            return vm;
        }

        public UserEditViewModel CreateUserEditViewModel(INavigationCoordinator navCoord, User userToEdit = null)
        {
            IUnitOfWork uow = uowFactory.BuildUOW();
            UserEditViewModel vm = new UserEditViewModel(navCoord, 
                                                         uow,
                                                         deleteStrategyFactory.BuildUserStrategy(),
                                                         modelValidatorFactory.BuildUserValidator());
            if (userToEdit != null)
            {
                vm.SetUser(userToEdit);
            }
            return vm;
        }

        public AssetDetailViewModel CreateAssetDetailViewModel(INavigationCoordinator navCoord, Asset asset = null)
        {
            IUnitOfWork uow = uowFactory.BuildUOW();
            AssetDetailViewModel vm = new AssetDetailViewModel(navCoord,
                                                               uow,
                                                               deleteStrategyFactory.BuildAssetStrategy(),
                                                               deleteStrategyFactory.BuildMetadataStrategy(),
                                                               modelValidatorFactory.BuildAssetValidator());
            if (asset != null)
            {
                vm.SetAsset(asset);
            }
            return vm;
        }

        public UserDashboardViewModel CreateUserDashboardViewModel(INavigationCoordinator navCoord)
        {
            IUnitOfWork uow = uowFactory.BuildUOW();
            UserDashboardViewModel vm = new UserDashboardViewModel(navCoord, uow);
            return vm;
        }

        public RoleEditViewModel CreateRoleEditViewModel(INavigationCoordinator navCoord, SecRole roleToEdit = null)
        {
            IUnitOfWork uow = uowFactory.BuildUOW();
            RoleEditViewModel vm = new RoleEditViewModel(navCoord,
                                                         uow,
                                                         deleteStrategyFactory.BuildRoleStrategy(),
                                                         modelValidatorFactory.BuildRoleValidator());
            if (roleToEdit != null)
            {
                vm.SetRole(roleToEdit);
            }
            return vm;
        }

        public ProjectSettingsViewModel CreateProjectSettingsViewModel(INavigationCoordinator navCoord)
        {
            IUnitOfWork uow = uowFactory.BuildUOW();
            ProjectSettingsViewModel vm = new ProjectSettingsViewModel(navCoord,
                                                                       uow, 
                                                                       MainViewModel.Instance.CurrentUser, 
                                                                       deleteStrategyFactory.BuildRoleStrategy(),
                                                                       deleteStrategyFactory.BuildAlertStrategy(),
                                                                       deleteStrategyFactory.BuildUserStrategy(),
                                                                       deleteStrategyFactory.BuildCategoryStrategy());
            return vm;
        }

        public AssetListViewModel CreateAssetListViewModel(INavigationCoordinator navCoord)
        {
            IUnitOfWork uow = uowFactory.BuildUOW();
            AssetListViewModel vm = new AssetListViewModel(navCoord,
                                                           uow, 
                                                           MainViewModel.Instance.CurrentUser,
                                                           deleteStrategyFactory.BuildAssetStrategy());
            return vm;
        }

        public CategoryEditViewModel CreateCategoryEditViewModel(INavigationCoordinator navCoord, AssetCategory cat = null)
        {
            IUnitOfWork uow = uowFactory.BuildUOW();
            CategoryEditViewModel vm = new CategoryEditViewModel(navCoord,
                                                                 uow, 
                                                                 deleteStrategyFactory.BuildCategoryStrategy(),
                                                                 modelValidatorFactory.BuildCategoryValidator());
            if (cat != null)
            {
                vm.SetCategory(cat);
            }

            return vm;
        }
    }
}
