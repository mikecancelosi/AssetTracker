using AssetTracker.Enums;
using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.ViewModels.Interfaces;
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

namespace AssetTracker.ViewModels
{
    public class AssetDetailViewModel : ViewModel, ISavable
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

        public List<Metadata> Tags
        {
            get
            {
                return myAsset.Metadata.Count > 0 ? myAsset.Metadata.ToList() : null;
            }
        }

        public List<Change> Changelog { get; set; }
        public ObservableCollection<AssetHierarchyObject> Hierarchy { get; set; }

        public string StatusFilter
        {
            get
            {
                if (myAsset.AssetCategory != null)
                {
                    return "select * from Phase where ph_caid = " + myAsset.AssetCategory.ca_id;
                }
                return "";
            }
        }
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

        //ISavable Properties
        public bool IsSavable => context.ChangeTracker.HasChanges();
        public ICommand DeleteConfirmed { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelSave { get; set; }
        public List<Violation> SaveViolations { get; set; }
        public NavigationCoordinator navCoordinator { get; set; }
        public bool PromptSave { get; set; }
        //TODO : Add back in hierarchy selected command
        public AssetDetailViewModel(NavigationCoordinator coord)
        {
            DeleteConfirmed = new RelayCommand((s) => DeleteAsset(), (s) => true);
            SaveCommand = new RelayCommand((s) => Save(), (s) => true);
            CancelSave = new RelayCommand((s) => navCoordinator.NavigateToQueued(), (s) => true);
        }

        public void DeleteAsset()
        {
            myAsset.Delete(context);
         
        }

        public void OnModelChanged()
        {
            NotifyPropertyChanged("myAsset");

            Changelog = (from s in context.Changes
                         where s.ch_recid == myasset.ID
                         select s).ToList();
            NotifyPropertyChanged("Changelog");
            NotifyPropertyChanged("DiscussionBoard");
            NotifyPropertyChanged("myAsset");
            NotifyPropertyChanged("Tags");
            SetHierarchy();
        }

        #region SetNewValues
        public void ModifyAssignedUser(int userID)
        {
            context.Entry(myAsset).Property(x => x.as_usid).CurrentValue = userID;
            NotifyPropertyChanged("Savable");
        }

        public void ModifyPhase(int phaseID)
        {
            context.Entry(myAsset).Property(x => x.as_phid).CurrentValue = phaseID;
            NotifyPropertyChanged("Savable");
        }

        public void ModifyCategory(int catID)
        {
            context.Entry(myAsset).Property(x => x.as_caid).CurrentValue = catID;
            NotifyPropertyChanged("Savable");
            NotifyPropertyChanged("myAsset");
        }

        public void ModifyAssetName(string newName)
        {
            context.Entry(myAsset).Property(x => x.as_displayname).CurrentValue = newName;
            NotifyPropertyChanged("myAsset");
            NotifyPropertyChanged("Savable");
        }

        public void ModifyDescription(string newDescription)
        {
            context.Entry(myAsset).Property(x => x.as_description).CurrentValue = newDescription;
            NotifyPropertyChanged("myAsset");
            NotifyPropertyChanged("Savable");
        }
        #endregion

        public void Save()
        {
            List<Change> changes = new List<Change>();
            List<Alert> alerts = new List<Alert>();
            var violations = new List<Violation>();
           
            if (context.Entry(myAsset).State == System.Data.Entity.EntityState.Modified)
            {
                Asset beforeAsset = context.Entry(myAsset).GetDatabaseValues().ToObject() as Asset;
                changes = myAsset.GetChanges(beforeAsset);
                alerts = myAsset.GetAlerts(beforeAsset);
            }
            if (myAsset.Save(context, out violations))
            {
                foreach (Change change in changes)
                {
                    change.Save(context, out violations);
                }
                foreach (Alert alert in alerts)
                {
                    //alert.Save(context);
                }
                NotifyPropertyChanged("Savable");
                NotifyPropertyChanged("Changelog");    
                if(navCoordinator.WaitingToNavigate)
                {
                    navCoordinator.NavigateToQueued();
                }
            }
            else
            {
                SaveViolations = violations;
                NotifyPropertyChanged("Violations");
                throw new NotImplementedException();
            }
          
        }

     

        #region Tags
        public void DeleteTag(int id)
        {
            Metadata data = context.Metadata.Find(id);
            myAsset.Metadata.Remove(data);
            context.Metadata.Remove(data);
            NotifyPropertyChanged("Savable");
            NotifyPropertyChanged("Tags");
        }

        public void AddMetadata(string text)
        {
            Metadata dataInst = context.Metadata.Create();
            dataInst.md_asid = myAsset.as_id;
            dataInst.md_value = text;
            myAsset.Metadata.Add(dataInst);
            context.Metadata.Add(dataInst);
            NotifyPropertyChanged("Savable");
            NotifyPropertyChanged("Tags");
        }
        #endregion

        #region Hierarchy
        public struct AssetHierarchyObject
        {
            public Asset asset { get; set; }
            public int level { get; set; }
            public System.Windows.Thickness marginFactor => new System.Windows.Thickness(level * 10, 0, 0, 10);
            public bool currentObject { get; set; }
        }


        private void SetHierarchy()
        {
            if (Hierarchy == null)
            {
                Hierarchy = new ObservableCollection<AssetHierarchyObject>();
            }

            Hierarchy.Clear();

            Asset parentAsset = myAsset;
            while (parentAsset.Parent != null)
            {
                parentAsset = parentAsset.Parent;
            }

            if (parentAsset.Children.Count > 0)
            {
                CreateHierarchyFromParent(parentAsset, 0).ForEach(x => Hierarchy.Add(x));
            }
            NotifyPropertyChanged("Hierarchy");
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

        public void OnHierarchyAssetSelected(int id)
        {
            Asset newAsset = context.Assets.Find(id);
            myAsset = newAsset;
        }

        #endregion

        #region PhaseTimeline
        public struct PhaseTimelineObject
        {
            public string PhaseName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string UserName { get; set; }

            public string UserLabel
            {
                get
                {
                    if (EndDate != null && EndDate < DateTime.Today)
                    {
                        return "Completed By:";
                    }
                    else if (StartDate != null && StartDate > DateTime.MinValue)
                    {
                        return "Being worked by:";
                    }
                    else if (UserName != "")
                    {
                        return "Assigned To:";
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            public string UserNameLabel
            {
                get
                {
                    return UserName == "" ? "Unassigned" : UserName;
                }
            }

            public string DateLabel
            {
                get
                {
                    if (EndDate != null && EndDate < DateTime.Today)
                    {
                        return StartDate.ToString("MM/dd") + "-" + EndDate.ToString("MM/dd");
                    }
                    else if (StartDate != null && StartDate > DateTime.MinValue)
                    {
                        return "Started " + StartDate.ToString("MM/dd");
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }

        private List<PhaseTimelineObject> phaseTimelineObjects;
        public List<PhaseTimelineObject> PhaseTimelineObjects
        {
            get
            {
                if (phaseTimelineObjects == null)
                {
                    phaseTimelineObjects = new List<PhaseTimelineObject>();
                    List<Phase> phases = (from p in context.Phases
                                          where p.ph_caid == myasset.as_caid
                                          orderby p.ph_step ascending
                                          select p).ToList();

                    if (phases.Count > 0)
                    {
                        List<Change> phaseChanges = (from c in context.Changes
                                                     where c.ch_recid == myAsset.as_id
                                                     && (c.ch_description == ChangeType.CreatedAsset
                                                     || c.ch_description == ChangeType.ChangedPhase
                                                     || c.ch_description == ChangeType.UserAssigned)
                                                     select c).ToList();

                        DateTime lastDate = DateTime.Now;
                        foreach (Phase phase in phases)
                        {
                            if (phase.ph_step == 1)
                            {
                                Change createChange = phaseChanges.FirstOrDefault(c => c.ch_description == ChangeType.CreatedAsset);
                                lastDate = createChange?.ch_datetime ?? DateTime.MinValue;
                            }

                            Change phaseChange = phaseChanges.FirstOrDefault(c => c.ch_description == ChangeType.ChangedPhase &&
                                                                                  c.ch_oldvalue == "1");

                            string username = "";

                            //TODO: Allow user to be assigned to phases individually.
                            if (phaseChanges.Any(c => c.ch_description == ChangeType.UserAssigned))
                            {
                                Change userAssignChange = phaseChanges.Last(c => c.ch_description == ChangeType.UserAssigned);
                                int userID = int.Parse(userAssignChange.ch_newvalue);
                                User userassigned = context.Users.Find(userID);
                                username = userassigned.Name;
                            }

                            PhaseTimelineObject timelineObject = new PhaseTimelineObject()
                            {
                                StartDate = lastDate,
                                EndDate = phaseChange?.ch_datetime ?? DateTime.MaxValue,
                                PhaseName = phase.ph_name,
                                UserName = username
                            };

                            phaseTimelineObjects.Add(timelineObject);
                            lastDate = timelineObject.EndDate;
                        }
                    }

                }
                return phaseTimelineObjects;
            }
        }


        #endregion

        public void CreateNewDiscussion(int parentID, string content)
        {
            Discussion newDiscussion = context.Discussions.Create();
            newDiscussion.di_contents = content;
            newDiscussion.di_date = DateTime.Now;
            newDiscussion.di_asid = myAsset.ID;
            newDiscussion.di_usid = MainViewModel.Instance.CurrentUser.us_id;
            if (parentID > 0)
            {
                newDiscussion.di_parentid = parentID;

                Discussion parentDiscussion = context.Discussions.Find(parentID);
                int parentUserId = parentDiscussion.di_usid ?? 0;
                if (parentUserId > 0 &&
                    parentUserId != MainViewModel.Instance.CurrentUser.us_id)
                {
                    //TODO: Create an alert for all users that have replied on the discussion
                    User parentUser = context.Users.Find(parentUserId);
                    Alert newAlert = context.Alerts.Create();
                    newAlert.ar_usid = parentDiscussion.di_usid;
                    newAlert.ar_projectwide = false;
                    newAlert.ar_asid = myAsset.as_id;
                    newAlert.ar_date = DateTime.Now;
                    newAlert.ar_type = AlertType.DiscussionReply;
                    newAlert.ar_header = parentUser.us_displayname + " continued the conversation on #" + myAsset.as_id;
                    newAlert.ar_content = content.Substring(0,47) + "...";

                    context.Alerts.Add(newAlert);
                }
            }

            context.Discussions.Add(newDiscussion);
            context.SaveChanges();
            NotifyPropertyChanged("DiscussionBoard");
        }       

    }
}
