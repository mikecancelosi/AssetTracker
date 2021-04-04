using AssetTracker.DAL;
using AssetTracker.Model;
using AssetTracker.Services;
using Mehdime.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AssetTracker.ViewModel
{
    public class SearchBoxViewModel
    {
        private ObservableCollection<DatabaseBackedObject> items;
        public ObservableCollection<DatabaseBackedObject> Items
        {
            get
            {
                if (items == null)
                {
                   
                }

                return items;
            }
        }

        public string Filter;
        private Type DboType;
        public string Label;
        public int ID;

        private GenericRepository<DatabaseBackedObject> assetRepo;
        private IDbContextScopeFactory scopeFactory;

        public SearchBoxViewModel(Type dbotype,string filter = "")
        {
            DboType = dbotype;
            Filter = filter;
            scopeFactory = new DbContextScopeFactory();
            assetRepo = new GenericRepository<DatabaseBackedObject>(scopeFactory);
        }

        public void SetProperties(string label, int id)
        {
            Label = label;
            ID = id;
        }
    }
}
