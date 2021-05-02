using AssetTracker.Model;
using AssetTracker.View.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace AssetTracker.ViewModel
{
    public class AssetDetailViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Debug display setup
        /// </summary>
        private Asset myasset = new Asset()
        {
            as_displayname = "Asset Name",
            as_id = 229,
            Children = new List<Asset>()
            {
                new Asset()
                {
                    as_displayname = "Child 1"
                },
                new Asset()
                {
                    as_displayname = "Child 2"
                }
            }
        };
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
                myasset = context.Assets.Find(value.ID);
                OnModelChanged();
            }
        }

        public string Metadata { get; private set; }
        public List<Change> Changelog { get; set; }
        public List<AssetHierarchyObject> Hierarchy { get; set; }

        public List<Discussion> DiscussionBoard
        {
            get
            {
                return (from d in context.Discussions
                        where (d.di_asid == myAsset.ID
                        && d.di_parentid == null)
                        select d).ToList();
            }
            set
            {
                DiscussionBoard = value;
                NotifyPropertyChanged("DiscussionBoard");
            }

        }


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
            List<Metadata> data = (from m in context.Metadata
                                   where m.md_recid == myAsset.ID
                                   select m).ToList();
            Metadata = string.Join(",", data.Select(x => x.md_value).ToList());
            NotifyPropertyChanged("Metadata");
        }

        #region HierarchySetup
        private void SetHierarchy()
        {
            if (Hierarchy == null)
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
                level = level,
                currentObject = refAsset.ID == myAsset.ID
            });

            foreach (Asset child in refAsset.Children)
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

        public string GetLatestAssetLink
        {
            get
            {
                if (myAsset != null)
                {
                    AssetLink al = (from l in context.AssetLinks
                                    where l.li_asid == myAsset.ID
                                    select l).FirstOrDefault();
                    if (al != null)
                    {
                        return "Lastest asset version can be found at : " + al.li_location;
                    }
                }

                return "File not uploaded yet";
            }
        }

        public bool OnSave(out List<Violation> violations)
        {
            List<Change> changes = new List<Change>();
            if (context.Entry(myAsset).State == System.Data.Entity.EntityState.Modified)
            {
                Asset beforeAsset = context.Entry(myAsset).GetDatabaseValues().ToObject() as Asset;
                changes = myAsset.GetChanges(beforeAsset);
            }
            if (myAsset.Save(context, out violations))
            {
                foreach (Change change in changes)
                {
                    change.Save(context, out violations);
                }
                return true;
            }
            return false;
        }
        public void CreateNewDiscussion(int parentID, string content)
        {
            Discussion newDiscussion = context.Discussions.Create();
            newDiscussion.di_contents = content;
            newDiscussion.di_date = DateTime.Now;
            newDiscussion.di_asid = myAsset.ID;
            newDiscussion.di_usid = 1;
            if (parentID > 0)
            {
                newDiscussion.di_parentid = parentID;
            }

            // TODO Assign discussion to current user.
            context.Discussions.Add(newDiscussion);
            context.SaveChanges();
            NotifyPropertyChanged("DiscussionBoard");
        }
   


        public struct AssetHierarchyObject
        {
            public Asset asset { get; set; }
            public int level { get; set; }
            public System.Windows.Thickness marginFactor => new System.Windows.Thickness(level * 10, 0, 0, 10);
            public bool currentObject { get; set; }
        }
    }
}
