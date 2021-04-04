using AssetTracker.DAL;
using AssetTracker.Model;
using AssetTracker.Services;
using Mehdime.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTracker.ViewModel
{
    public class AssetCreateViewModel
    {
        private GenericRepository<Asset> assetRepo;
        private IDbContextScopeFactory scopeFactory;

        public AssetCreateViewModel()
        {
            scopeFactory = new DbContextScopeFactory();
            assetRepo = new GenericRepository<Asset>(scopeFactory);
        }

        public bool CreateAsset(string name, string description, int categoryID, out List<Violation> violations, int assignedToID = 0, int parentID = 0)
        {
            Asset asset = new Asset()
            {
                as_displayname = name,
                as_description = description,
                as_caid = categoryID,
                as_usid = assignedToID,
            };
            if (asset.IsValid(out violations))
            {
                assetRepo.Insert(asset);
                return true;
            }
            else
            {
                //Handle Violations
                return false;
            }
        }
    }
}
