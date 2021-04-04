using AssetTracker.DAL;
using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View;
using Mehdime.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Linq;

namespace AssetTracker.ViewModel
{
    public class AssetListViewModel
    {
        private ObservableCollection<Asset> assets;
        public ObservableCollection<Asset> Assets
        {
            get
            {
                if(assets == null)
                {
                    assets = new ObservableCollection<Asset>();
                    assetRepo.Get().ToList().ForEach(a => assets.Add(a));
                }
               
                return assets;                                   
            }
        }

        private GenericRepository<Asset> assetRepo;
        private IDbContextScopeFactory scopeFactory;

        public AssetListViewModel()
        {
            scopeFactory = new DbContextScopeFactory();
            assetRepo = new GenericRepository<Asset>(scopeFactory);
        }

        public void AddItem()
        {
           
        }

        


    }
}
