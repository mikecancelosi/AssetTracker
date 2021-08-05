﻿using AssetTracker.Services;
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
        private readonly IDeleteStrategy<Asset> assetDeleteStrategy;

        public AssetListViewModel(INavigationCoordinator coord, GenericUnitOfWork uow, IDeleteStrategy<Asset> assetDeleteStrat)
        {
            navCoordinator = coord;
            assetDeleteStrategy = assetDeleteStrat;
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
            assetDeleteStrategy.Delete(unitOfWork, CurrentSelectedAsset);           
            unitOfWork.Commit();
            CurrentSelectedAsset = null;
            NotifyPropertyChanged("CurrentSelectedAsset");
            NotifyPropertyChanged("Assets");
        }
        #endregion


    }
}
