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
    public class ViewModelFactory : IViewModelFactory
    {       
        public IDeleteStrategy<User> userDeleteStrategy { get; private set; }

        public IDeleteStrategy<Asset> assetDeleteStrategy { get; private set; }

        public IDeleteStrategy<SecRole> roleDeleteStrategy { get; private set; }

        public IDeleteStrategy<AssetCategory> catDeleteStrategy { get; private set; }

        public IDeleteStrategy<Alert> alertDeleteStrategy { get; private set; }

        public IDeleteStrategy<Metadata> tagDeleteStrategy { get; private set; }

        public ViewModelFactory(IDeleteStrategy<User> userDeleteStrategy,
                                IDeleteStrategy<Asset> assetDeleteStrategy,
                                IDeleteStrategy<SecRole> roleDeleteStrategy,
                                IDeleteStrategy<AssetCategory> catDeleteStrategy,
                                IDeleteStrategy<Alert> alertDeleteStrategy,
                                IDeleteStrategy<Metadata> tagDeleteStrategy)
        {
            this.userDeleteStrategy = userDeleteStrategy;
            this.assetDeleteStrategy = assetDeleteStrategy;
            this.roleDeleteStrategy = roleDeleteStrategy;
            this.catDeleteStrategy = catDeleteStrategy;
            this.alertDeleteStrategy = alertDeleteStrategy;
            this.tagDeleteStrategy = tagDeleteStrategy;
        }

        public LoginViewModel CreateLoginViewModel(INavigationCoordinator navCoord)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            LoginViewModel vm = new LoginViewModel(navCoord, uow);
            return vm;
        }

        public UserEditViewModel CreateUserEditViewModel(INavigationCoordinator navCoord, User userToEdit = null)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            UserEditViewModel vm = new UserEditViewModel(navCoord, uow, userDeleteStrategy);
            if (userToEdit != null)
            {
                vm.SetUser(userToEdit);
            }
            return vm;
        }

        public AssetDetailViewModel CreateAssetDetailViewModel(INavigationCoordinator navCoord, Asset asset = null)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            AssetDetailViewModel vm = new AssetDetailViewModel(navCoord, uow, assetDeleteStrategy, tagDeleteStrategy);
            if (asset != null)
            {
                vm.SetAsset(asset);
            }
            return vm;
        }

        public UserDashboardViewModel CreateUserDashboardViewModel(INavigationCoordinator navCoord)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            UserDashboardViewModel vm = new UserDashboardViewModel(navCoord, uow);
            return vm;
        }

        public RoleEditViewModel CreateRoleEditViewModel(INavigationCoordinator navCoord, SecRole roleToEdit = null)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            RoleEditViewModel vm = new RoleEditViewModel(navCoord, uow, roleDeleteStrategy);
            if (roleToEdit != null)
            {
                vm.SetRole(roleToEdit);
            }
            return vm;
        }

        public ProjectSettingsViewModel CreateProjectSettingsViewModel(INavigationCoordinator navCoord)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            ProjectSettingsViewModel vm = new ProjectSettingsViewModel(navCoord, uow, MainViewModel.Instance.CurrentUser, roleDeleteStrategy, alertDeleteStrategy, userDeleteStrategy, catDeleteStrategy);
            return vm;
        }

        public AssetListViewModel CreateAssetListViewModel(INavigationCoordinator navCoord)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            AssetListViewModel vm = new AssetListViewModel(navCoord, uow, MainViewModel.Instance.CurrentUser, assetDeleteStrategy);
            return vm;
        }

        public CategoryEditViewModel CreateCategoryEditViewModel(INavigationCoordinator navCoord, AssetCategory cat = null)
        {
            GenericUnitOfWork uow = new GenericUnitOfWork(new TrackerContext());
            CategoryEditViewModel vm = new CategoryEditViewModel(navCoord, uow, catDeleteStrategy);
            if (cat != null)
            {
                vm.SetCategory(cat);
            }
            return vm;
        }
    }
}
