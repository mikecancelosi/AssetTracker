
using AssetTracker.Model;
using AssetTracker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Text;
using System.Linq;

namespace AssetTracker.ViewModel
{
    public class AssetCreateViewModel : ViewModel
    {
        private Asset assetToCreate;
        public Asset AssetToCreate { get { return assetToCreate; } set { assetToCreate = value; } }


        public AssetCreateViewModel()
        {
            assetToCreate = context.Assets.Create();
            context.Assets.Add(assetToCreate);
        }

        public void OnParentAssetChanged(DatabaseBackedObject newSelection)
        {
            Asset parent = (from asset in context.Assets
                            where asset.as_id == newSelection.ID
                            select asset).FirstOrDefault();

            context.Entry(assetToCreate).Property(x=>x.as_parentid).CurrentValue = (parent.ID);
        }

        public void OnCategoryChanged(DatabaseBackedObject newSelection)
        {
            AssetCategory category = (from cat in context.AssetCategories
                                      where cat.ca_id == newSelection.ID
                                      select cat).FirstOrDefault();
            context.Entry(assetToCreate).Property(x => x.as_caid).CurrentValue = category.ID;
        }

        public void OnUserChanged(DatabaseBackedObject newSelection)
        {
            User user = (from u in context.Users
                         where u.us_id == newSelection.ID
                         select u).FirstOrDefault();

            context.Entry(assetToCreate).Property(x => x.as_usid).CurrentValue = user.ID;
        }

        public bool CreateAsset(out List<Violation> violations)
        {
            if (assetToCreate.Save(context, out violations))
            {
                Change change = new Change()
                {
                    ch_datetime = DateTime.Now,
                    ch_description = "Asset Created",
                    ch_recid = assetToCreate.as_id,
                };

                if(change.Save(context,out violations))
                {
                    return true;
                }
            }

            return false;
        }
       
    }
}
