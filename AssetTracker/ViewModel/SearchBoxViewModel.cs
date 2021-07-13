using AssetTracker.Model;
using AssetTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace AssetTracker.ViewModel
{
    public class SearchBoxViewModel : ViewModel
    {
        private List<DatabaseBackedObject> items;
        public List<DatabaseBackedObject> Items
        {
            get
            {
                if (items == null && DboType != null)
                {
                    items = new List<DatabaseBackedObject>();
                    foreach(var x in context.Set(DboType))
                    {

                        items.Add((DatabaseBackedObject)x);
                    }
                               
                }

                return items;
            }
        }

        private DatabaseBackedObject currentlySelectedObject;
        public DatabaseBackedObject CurrentlySelectedObject
        {
            get
            {
                return currentlySelectedObject;
            }
            set
            {
                currentlySelectedObject = value;
                NotifyPropertyChanged("CurrentlySelectedObject");
            }
        }

        //TODO: Applying a filter to searchbox results
        public string Filter;
        private Type DboType;

        public SearchBoxViewModel()
        {

        }

        public SearchBoxViewModel(Type dbotype, string filter = "")
        {
            DboType = dbotype;
            Filter = filter;
            currentlySelectedObject = Activator.CreateInstance(dbotype) as DatabaseBackedObject;
        }

      
        public void SelectionChanged(int id)
        {
            if (id > 0)
            {
                DatabaseBackedObject copyFrom = context.Set(DboType).Find(id) as DatabaseBackedObject;
                DatabaseBackedObject.CopyProperties(copyFrom, CurrentlySelectedObject);
                NotifyPropertyChanged("CurrentlySelectedObject");
            }
        }         
    }
}
