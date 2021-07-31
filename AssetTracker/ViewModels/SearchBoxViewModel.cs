using AssetTracker.Model;
using AssetTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModels
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
            get => currentlySelectedObject;
            private set
            {
                currentlySelectedObject = value;
                NotifyPropertyChanged("CurrentlySelectedObject");
            }
        }

        public List<DatabaseBackedObject> FilteredItems
        {
            get
            {               
                return Items?.Where(x => x.ID.ToString().ToLower().Contains(UserFilter.ToLower())
                                         || x.Name.ToLower().Contains(UserFilter.ToLower()))
                                         .ToList() ?? new List<DatabaseBackedObject>();
            }
        }

        /// <summary>
        /// Filter set in initialization of the seachbox
        /// </summary>
        public string BaseFilter { get; set; }
        /// <summary>
        /// Filter set by the user trying to search results through the textbox
        /// </summary>
        public string UserFilter { get; set; }
        private Type DboType;
        public event Action OnSelectionChanged;
        public SearchBoxViewModel(Type dbotype)
        {
            DboType = dbotype;
            UserFilter = "";
            CurrentlySelectedObject = (DatabaseBackedObject) context.Set(dbotype).Create();
        }        

        public void SetSelectedItem(int id)
        {
            if (id > 0)
            {
                DatabaseBackedObject copyFrom = Items.Find(dbo => dbo.ID == id);

                if(CurrentlySelectedObject == null) // Can't copy to a null value
                {
                    CurrentlySelectedObject = (DatabaseBackedObject)context.Set(DboType).Create();
                }

                DatabaseBackedObject.CopyProperties(copyFrom, CurrentlySelectedObject);
            }
            else
            {
                CurrentlySelectedObject = (DatabaseBackedObject)context.Set(DboType).Create();
            }
            SetUserFilter("");
            NotifyPropertyChanged("CurrentlySelectedObject");
            OnSelectionChanged?.Invoke();
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

        public void SetUserFilter(string newValue)
        {
            UserFilter = newValue;
            NotifyPropertyChanged("UserFilter");
            NotifyPropertyChanged("FilteredItems");
        }

        public void CheckFilter()
        {
           //TODO: Add ability to clear selection.
           var dbo =  Items.FirstOrDefault(x => x.ID.ToString() == UserFilter || x.Name == UserFilter);
           if(dbo != null)
            {
                CurrentlySelectedObject = dbo;
                UserFilter = "";
                NotifyPropertyChanged("UserFilter");
                NotifyPropertyChanged("CurrentlySelectedObject");
            }
        }      

        public void ClearInvocationList()
        {
            foreach (Delegate d in OnSelectionChanged.GetInvocationList())
            {
                OnSelectionChanged -= (Action)d;
            }
        }

        public void ResetDisplay()
        {
            CurrentlySelectedObject = null;
            BaseFilter = "";
            UserFilter = "";
            Items = null;
            NotifyPropertyChanged("CurrentlySelectedObject");
            NotifyPropertyChanged("BaseFilter");
            NotifyPropertyChanged("UserFilter");
            NotifyPropertyChanged("FilteredItems");
        }
    }
}
