using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Linq;

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

        #region AddAsset
        public Asset AssetToCreate { get; set; }

        public void OnNameChanged(string newName)
        {
            if (newName != "")
            {
                context.Entry(AssetToCreate).Property(x => x.as_displayname).CurrentValue = newName;
            }
        }

        public void OnDescriptionChanged(string newDescription)
        {
            if (newDescription != "")
            {
                context.Entry(AssetToCreate).Property(x => x.as_description).CurrentValue = newDescription;
            }
        }
        
        public void OnParentAssetChanged(int id)
        {
            Asset parent = context.Assets.Find(id);
            context.Entry(AssetToCreate).Property(x => x.as_parentid).CurrentValue = (parent.ID);
        }

        public void OnCategoryChanged(int id)
        {
            AssetCategory category = context.AssetCategories.Find(id);
            context.Entry(AssetToCreate).Property(x => x.as_caid).CurrentValue = category.ID;
        }

        public void OnUserChanged(int id)
        {
            User user = context.Users.Find(id);
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
                    ResetNewAsset();
                    NotifyPropertyChanged("AssetToCreate");
                    NotifyPropertyChanged("Assets");
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
        #endregion

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
