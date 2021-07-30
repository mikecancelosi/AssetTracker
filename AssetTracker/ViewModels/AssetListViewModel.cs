using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Windows.Input;
using AssetTracker.View.Commands;

namespace AssetTracker.ViewModels
{
    public class AssetListViewModel : ViewModel
    {       
        public List<Asset> Assets
        {
            get
            {
                return (from a in context.Assets
                        select a).ToList();
            }
        }

        public ICommand CreateAssetCommand { get; set; }

        private readonly INavigationCoordinator navCoordinator;
        public AssetListViewModel(INavigationCoordinator coord)
        {
            navCoordinator = coord;
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
            CurrentSelectedAsset.Delete(context);
            NotifyPropertyChanged("CurrentSelectedAsset");
            NotifyPropertyChanged("Assets");
        }
        #endregion


    }
}
