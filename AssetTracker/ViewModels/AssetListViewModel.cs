using AssetTracker.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AssetTracker.View.Commands;
using DomainModel;
using DataAccessLayer;
using DataAccessLayer.Strategies;

namespace AssetTracker.ViewModels
{
    public class AssetListViewModel : ViewModel
    {       
        /// <summary>
        /// Assets to display 
        /// </summary>
        public List<Asset> Assets =>  (from a in assetRepo.Get() 
                                       select a).ToList();

        /// <summary>
        /// The user wants to create a new asset. Navigate to create asset page
        /// </summary>
        public ICommand CreateAssetCommand => new RelayCommand((p) => navCoordinator.NavigateToCreateAsset(), (p) => true);
        public ICommand DeleteAssetCommand => new RelayCommand((p) => DeleteCurrentlySelectedAsset(), (p) => true);

        /// <summary>
        /// Repo to get the asset list from
        /// </summary>
        private GenericRepository<Asset> assetRepo;
        /// <summary>
        /// Coordinator instance top use to navigate away
        /// </summary>
        private readonly INavigationCoordinator navCoordinator;
        /// <summary>
        /// Strategy to use to delete any asset
        /// </summary>
        private readonly IDeleteStrategy<Asset> assetDeleteStrategy;

        #region Permissions
        public bool IsAbleToDeleteAsset => PermissionsManager.HasPermission(LoggedInUser, Permission.DeleteAsset, unitOfWork);
        public bool IsAbleToCreateAsset => PermissionsManager.HasPermission(LoggedInUser, Permission.CreateAsset, unitOfWork);
        #endregion
        
        /// <summary>
        /// Currently logged in user
        /// </summary>
        private User LoggedInUser;

        public AssetListViewModel(INavigationCoordinator coord,
                                  GenericUnitOfWork uow,
                                  User loggedInUser,
                                  IDeleteStrategy<Asset> assetDeleteStrat)
        {
            navCoordinator = coord;
            assetDeleteStrategy = assetDeleteStrat;
            unitOfWork = uow;
            LoggedInUser = loggedInUser;

            assetRepo = unitOfWork.GetRepository<Asset>();
        }

        /// <summary>
        /// User wants to modify the given asset
        /// </summary>
        /// <param name="ast">Asset to modify</param>
        public void NavigateToAssetDetail(Asset ast)
        {
            navCoordinator.NavigateToAssetDetail(ast);
        }

        #region DeleteAsset
        /// <summary>
        /// Backing field for currentselectedasset
        /// </summary>
        private Asset currentlySelectedAsset;
        /// <summary>
        /// Currently selected asset on the assetlist
        /// </summary>
        public Asset CurrentSelectedAsset 
        {
            get => currentlySelectedAsset;
            set
            {
                currentlySelectedAsset = value;
                NotifyPropertyChanged("CurrentSelectedAsset");
            }
        }

        /// <summary>
        /// Delete the currently selected asset and commit to db.
        /// </summary>
        public void DeleteCurrentlySelectedAsset()
        {
            assetDeleteStrategy.Delete(unitOfWork, CurrentSelectedAsset);           
            unitOfWork.Commit();
            CurrentSelectedAsset = null;
            NotifyPropertyChanged("CurrentSelectedAsset");
            NotifyPropertyChanged("Assets");
        }
        #endregion


    }
}
