using AssetTracker.Model;
using AssetTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AssetTracker.ViewModel
{
    public class SearchBoxViewModel
    {
        private TrackerContext context = new TrackerContext();

        private List<DatabaseBackedObject> items;
        public List<DatabaseBackedObject> Items
        {
            get
            {
                if (items == null)
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

        public string Filter;
        private Type DboType;
        public string Label;
        public int ID;
       
        public SearchBoxViewModel(Type dbotype,string filter = "")
        {
            DboType = dbotype;
            Filter = filter;
        }

        public void SetProperties(string label, int id)
        {
            Label = label;
            ID = id;
        }
    }
}
