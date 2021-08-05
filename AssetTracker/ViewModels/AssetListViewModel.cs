using AssetTracker.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AssetTracker.View.Commands;
using DomainModel;
using DataAccessLayer;

namespace AssetTracker.ViewModels
{
    public class AssetListViewModel : ViewModel
    {       
        public List<Asset> Assets
        {
            get
            {
                return (from a in assetRepo.Get()
                        select a).ToList();
            }
        }

        public ICommand CreateAssetCommand { get; set; }

        private GenericRepository<Asset> assetRepo;
        private readonly INavigationCoordinator navCoordinator;
        public AssetListViewModel(INavigationCoordinator coord, GenericUnitOfWork uow)
        {
            navCoordinator = coord;
            unitOfWork = uow;
            assetRepo = unitOfWork.GetRepository<Asset>();
            CreateAssetCommand = new RelayCommand((p) => navCoordinator.NavigateToCreateAsset(), (p) => true);
        }

        public void NavigateToAssetDetail(Asset ast)
        {
            navCoordinator.NavigateToAssetDetail(ast);
        }

        #region DeleteAsset
        public Asset CurrentSelectedAsset { get; set; }
        public void ChangeSelectedAsset(Asset newAsset)
        {
            CurrentSelectedAsset = newAsset;
            NotifyPropertyChanged("CurrentSelectedAsset");
        }

        public void DeleteCurrentlySelectedAsset()
        {
            assetRepo.Delete(CurrentSelectedAsset);
            unitOfWork.Commit();
            NotifyPropertyChanged("CurrentSelectedAsset");
            NotifyPropertyChanged("Assets");
        }
        #endregion


    }
}
