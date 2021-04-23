using AssetTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace AssetTracker.ViewModel
{
    public class AssetDetailViewModel : INotifyPropertyChanged
    {
        private Asset myasset;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        private TrackerContext context = new TrackerContext();

        public Asset myAsset
        {
            get
            {
                return myasset;
            }
            set
            {
                myasset = value;
                OnModelChanged();
            }
        }

        public string Metadata { get; private set; }
        public List<Change> Changelog { get; set; }
        public List<AssetHierarchyObject> Hierarchy { get; set; }


        public void OnModelChanged()
        {
            NotifyPropertyChanged("myAsset");

            Changelog = (from s in context.Changes
                         where s.ch_recid == myasset.ID
                         select s).ToList();
            NotifyPropertyChanged("Changelog");

            SetMetadata();
            SetHierarchy();

        }

        public void SetMetadata()
        {
            List<Metadata> data = (from m in context.Metadatas
                                   where m.md_recid == myAsset.ID
                                   select m).ToList();
            Metadata = string.Join(",", data.Select(x => x.md_value).ToList());
            NotifyPropertyChanged("Metadata");
        }

        #region HierarchySetup
        private void SetHierarchy()
        {
            if(Hierarchy == null)
            {
                Hierarchy = new List<AssetHierarchyObject>();
            }

            Hierarchy.Clear();

            Asset parentAsset = myAsset;
            while (parentAsset.Parent != null)
            {
                parentAsset = parentAsset.Parent;
            }

            if (parentAsset.Children.Count > 0)
            {
                Hierarchy.AddRange(CreateHierarchyFromParent(parentAsset, 0));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hierarchy"));
        }       

        private List<AssetHierarchyObject> CreateHierarchyFromParent(Asset refAsset, int level)
        {
            List<AssetHierarchyObject> objectList = new List<AssetHierarchyObject>();
            objectList.Add(new AssetHierarchyObject()
            {
                asset = refAsset,
                level = level
            });

            foreach(Asset child in refAsset.Children)
            {
                objectList.AddRange(CreateHierarchyFromParent(child, level + 1));
            }

            return objectList;
        }
        #endregion

        public void OnHierarchyAssetSelected(int id)
        {

        }

        public void OnAssignedUserChange(int userID)
        {
            context.Entry(myAsset).Property(x => x.as_usid).CurrentValue = userID;
        }

        public void OnPhaseChanged(int phaseID)
        {
            context.Entry(myAsset).Property(x => x.as_phid).CurrentValue = phaseID;
        }
    }
    public struct AssetHierarchyObject
    {
        public Asset asset { get; set; }
        public int level { get; set; }
        public System.Windows.Thickness marginFactor => new System.Windows.Thickness(level * 10, 0, 0, 10);
    }
}
