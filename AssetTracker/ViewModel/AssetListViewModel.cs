using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Linq;

namespace AssetTracker.ViewModel
{
    public class AssetListViewModel : ViewModel
    {
        private List<Asset> assets;
        public List<Asset> Assets
        {
            get
            {
                if (assets == null)
                {
                    assets = new List<Asset>();
                    var temp = (from a in context.Assets
                                select a).ToList();
                    assets = temp;
                }

                return assets;
            }
        }        
              
        public Asset AssetToCreate { get; set; }       

        public void OnParentAssetChanged(DatabaseBackedObject newSelection)
        {
            Asset parent = (from asset in context.Assets
                            where asset.as_id == newSelection.ID
                            select asset).FirstOrDefault();

            context.Entry(AssetToCreate).Property(x => x.as_parentid).CurrentValue = (parent.ID);
        }

        public void OnCategoryChanged(DatabaseBackedObject newSelection)
        {
            AssetCategory category = (from cat in context.AssetCategories
                                      where cat.ca_id == newSelection.ID
                                      select cat).FirstOrDefault();
            context.Entry(AssetToCreate).Property(x => x.as_caid).CurrentValue = category.ID;
        }

        public void OnUserChanged(DatabaseBackedObject newSelection)
        {
            User user = (from u in context.Users
                         where u.us_id == newSelection.ID
                         select u).FirstOrDefault();

            context.Entry(AssetToCreate).Property(x => x.as_usid).CurrentValue = user.ID;
        }

        public void CreateAsset()
        {
            AssetToCreate = context.Assets.Create();
        }

        public bool SaveAsset(out List<Violation> violations)
        {
            context.Assets.Add(AssetToCreate);
            if (AssetToCreate.Save(context, out violations))
            {
                Change change = new Change()
                {
                    ch_datetime = DateTime.Now,
                    ch_description = "Asset Created",
                    ch_recid = AssetToCreate.as_id,
                };

                if (change.Save(context, out violations))
                {
                    return true;
                }
            }

            return false;
        }

        public void ResetNewAsset()
        {
            AssetToCreate = null;
            NotifyPropertyChanged("AssetToCreate");
            
        }

    }
}
