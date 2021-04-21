
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
    public class AssetCreateViewModel : INotifyPropertyChanged
    {
        private TrackerContext context = new TrackerContext();

        private Asset assetToCreate;
        public Asset AssetToCreate { get { return assetToCreate; } set { assetToCreate = value; } }

        public event PropertyChangedEventHandler PropertyChanged;

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
            try
            {
                if (assetToCreate.IsValid(out violations))
                {
                    if (context.Entry(assetToCreate).State == System.Data.Entity.EntityState.Detached)
                    {
                        context.Assets.Add(assetToCreate);
                    }
                    context.SaveChanges();
                    Change change;
                    if (CreateChange(assetToCreate, out violations, out change))
                    {
                        context.Changes.Add(change);
                        context.SaveChanges();

                        return true;
                    }

                }
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
            catch (InvalidOperationException e)
            {
                throw;
            }



            //Handle Violations
            return false;

        }

        private bool CreateChange(Asset asset, out List<Violation> violations, out Change change)
        {
            change = new Change()
            {
                ch_datetime = DateTime.Now,
                ch_description = "Asset Created",
                ch_recid = asset.as_id,
            };

            return change.IsValid(out violations);
        }
    }
}
