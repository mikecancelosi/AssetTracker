﻿
using AssetTracker.Model;
using AssetTracker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Text;

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
            assetToCreate = new Asset()
            {
                AssetCategory = new AssetCategory(),
                Parent = new Asset(),
                AssignedToUser = new User()

            };
            
        }

        public bool CreateAsset(out List<Violation> violations)
        {
            try
            {
                if (assetToCreate.IsValid(out violations))
                {

                    context.Assets.Add(assetToCreate);
                    context.Entry(assetToCreate).State = System.Data.Entity.EntityState.Added;
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
            catch(DbEntityValidationException e)
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
