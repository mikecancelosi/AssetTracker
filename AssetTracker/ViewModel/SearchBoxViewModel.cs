using AssetTracker.Model;
using AssetTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace AssetTracker.ViewModel
{
    public class SearchBoxViewModel : INotifyPropertyChanged
    {
        private TrackerContext context = new TrackerContext();

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

        public string Filter;
        private Type DboType;

        public event PropertyChangedEventHandler PropertyChanged;
        public SearchBoxViewModel()
        {

        }

        public SearchBoxViewModel(Type dbotype, string filter = "")
        {
            DboType = dbotype;
            Filter = filter;
        }

        public SearchBoxViewModel(DatabaseBackedObject syncObject)
        {
            DboType = syncObject.GetType();
            currentlySelectedObject = syncObject;
        }


        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

      
        public void SelectionChanged(int id)
        {
            DatabaseBackedObject copyFrom = context.Set(DboType).Find(id) as DatabaseBackedObject;
            DatabaseBackedObject.CopyProperties(copyFrom, CurrentlySelectedObject);
            NotifyPropertyChanged("CurrentlySelectedObject");
        }
         
    }
}
