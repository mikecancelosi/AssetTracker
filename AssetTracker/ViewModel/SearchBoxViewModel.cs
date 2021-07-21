using AssetTracker.Model;
using AssetTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    foreach (var x in context.Set(DboType))
                    {
                        items.Add((DatabaseBackedObject)x);
                    }
                }

                return items;
            }
            set
            {
                items = value;
                NotifyPropertyChanged("Items");
                NotifyPropertyChanged("FilteredItems");

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

        public List<DatabaseBackedObject> FilteredItems
        {
            get
            {
                return Items?.Where(x => x.ID.ToString().ToLower().Contains(UserFilter.ToLower()) || x.Name.ToLower().Contains(UserFilter.ToLower())).ToList() ?? new List<DatabaseBackedObject>();
            }
        }

        //TODO: Applying a filter to searchbox results

        public string BaseFilter { get; set; }
        public string UserFilter { get; set; }
        private Type DboType;

        public SearchBoxViewModel()
        {

        }

        public SearchBoxViewModel(Type dbotype, string filter = "")
        {
            DboType = dbotype;
            UserFilter = filter;
            currentlySelectedObject = Activator.CreateInstance(dbotype) as DatabaseBackedObject;
        }


        public void SelectionChanged(int id)
        {
            if (id > 0)
            {
                DatabaseBackedObject copyFrom = context.Set(DboType).Find(id) as DatabaseBackedObject;
                DatabaseBackedObject.CopyProperties(copyFrom, CurrentlySelectedObject);
                NotifyPropertyChanged("CurrentlySelectedObject");
                ResetUserFilter();


            }
        }

        public void ResetUserFilter()
        {
            UserFilter = CurrentlySelectedObject.Name;
            NotifyPropertyChanged("UserFilter");
        }

        public void SetUserFilter(string newValue)
        {
            UserFilter = newValue;
            NotifyPropertyChanged("FilteredItems");

        }

        public async Task SetBaseFilter(string newFilter)
        {
            BaseFilter = newFilter;
            if ((BaseFilter ?? "") != "")
            {
                var query = context.Set(DboType).SqlQuery(BaseFilter);
                List<object> results = await context.Database.SqlQuery(DboType, BaseFilter).ToListAsync();
                List<DatabaseBackedObject> convertedResults = new List<DatabaseBackedObject>();
                results.ForEach(x => convertedResults.Add((DatabaseBackedObject)x));
                Items = convertedResults;

            }
        }


    }
}
