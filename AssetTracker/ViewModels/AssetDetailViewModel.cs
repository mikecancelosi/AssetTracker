using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.ViewModels.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Strategies;
using DomainModel;
using DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AssetTracker.ViewModels
{
    public class AssetDetailViewModel : ViewModel, ISavable
    {
        public Asset myAsset { get; private set; }

        public List<Metadata> Tags =>  myAsset.Metadata.Count > 0 ? myAsset.Metadata.ToList() : null;
        public List<Change> Changelog { get; set; }
        public ObservableCollection<AssetHierarchyObject> Hierarchy { get; set; }

      
        public List<Discussion> DiscussionBoard
        {
            get
            {
                return (from d in discussionRepo.Get()
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
        public bool IsSavable => unitOfWork.HasChanges;
        public ICommand SaveCommand { get; set; }
        public ICommand RefuseSave { get; set; }
        public List<Violation> SaveViolations { get; set; }
        public INavigationCoordinator navCoordinator { get; set; }
        private IDeleteStrategy<Asset> assetDeleteStrategy;
        private IDeleteStrategy<Metadata> tagDeleteStrategy;
        private bool promptSave;
        public bool PromptSave
        {
            get => promptSave;
            set
            {
                promptSave = value;
                NotifyPropertyChanged("PromptSave");
            }
        }

        public ICommand DeleteConfirmed { get; set; }

        private bool Creating;
        private bool Cloning;
        public string HeadingText
        {
            get
            {
                if (Creating)
                {
                    return "Creating Asset";
                }
                else if (Cloning)
                {
                    return "Cloning Asset";
                }
                return "Modify Asset";
            }
        }

        public string AssetTitle
        {
            get
            {
                string title = myAsset?.Name ?? "";
                return title != "" ? title : "New Asset";
            }
            set
            {
                myAsset.as_displayname = value;
                assetRepo.Update(myAsset);
                NotifyPropertyChanged("AssetTitle");
                NotifyPropertyChanged("IsSavable");
            }
        }

        public string Description
        {
            get
            {
                string description = myAsset?.as_description ?? "";
                return description != "" ? description : "Add a short blurb to describe this new asset!";
            }
            set
            {
                myAsset.as_description = value;
                assetRepo.Update(myAsset);
                NotifyPropertyChanged("Description");
                NotifyPropertyChanged("IsSavable");
            }
        }

        public AssetCategory Category
        {
            get => myAsset.AssetCategory;
            set
            {
                myAsset.AssetCategory = value;
                myAsset.Phase = null;
                assetRepo.Update(myAsset);
                NotifyPropertyChanged("Category");
                NotifyPropertyChanged("IsSavable");
                NotifyPropertyChanged("PhaseTimelineObjects");
            }
        }

        public User AssignedUser
        {
            get => myAsset.AssignedToUser;
            set
            {
                myAsset.AssignedToUser = value;
                myAsset.as_usid = value.ID;
                assetRepo.Update(myAsset);
                NotifyPropertyChanged("AssignedUser");
                NotifyPropertyChanged("IsSavable");
            }
        }

        public Phase CurrentPhase
        {
            get => myAsset.Phase;
            set
            {
                myAsset.Phase = value;
                assetRepo.Update(myAsset);
                NotifyPropertyChanged("AssignedUser");
                NotifyPropertyChanged("IsSavable");
            }
        }

      
        public List<DatabaseBackedObject> Users
        {
            get
            {
               return (from u in usersRepo.Get()
                               select u).ToList<DatabaseBackedObject>();               
            }
        }

        public List<DatabaseBackedObject> Categories
        {
            get
            {
                return (from c in categoryRepo.Get()                              
                               select c).ToList<DatabaseBackedObject>();
            }
        }

        public List<DatabaseBackedObject> Phases
        {
            get
            {
               return (from p in phaseRepo.Get()
                               where p.Category.ca_id == (Category?.ca_id ?? 0)         
                               select p).ToList<DatabaseBackedObject>();
            }
        }

        public ICommand DiscussionReplyClicked => new RelayCommand((arr) => CreateNewDiscussio(arr), (arr) => { return true; });

        private GenericRepository<Asset> assetRepo;
        private GenericRepository<Discussion> discussionRepo;
        private GenericRepository<Change> changesRepo;
        private GenericRepository<Alert> alertsRepo;
        private GenericRepository<Metadata> tagsRepo;
        private GenericRepository<User> usersRepo;
        private GenericRepository<Phase> phaseRepo;
        private GenericRepository<AssetCategory> categoryRepo;

        private User CurrentUser;


        //TODO : Add back in hierarchy selected command
        public AssetDetailViewModel(INavigationCoordinator coord, GenericUnitOfWork uow, IDeleteStrategy<Asset> assetDeleteStrat, IDeleteStrategy<Metadata> tagDeleteStrat)
        {
            navCoordinator = coord;
            assetDeleteStrategy = assetDeleteStrat;
            tagDeleteStrategy = tagDeleteStrat;
            navCoordinator.UserNavigationAttempt += (s) => PromptSave = true;

            unitOfWork = uow;
            assetRepo = unitOfWork.GetRepository<Asset>();
            discussionRepo = unitOfWork.GetRepository<Discussion>();
            changesRepo = unitOfWork.GetRepository<Change>();
            alertsRepo = unitOfWork.GetRepository<Alert>();
            tagsRepo = unitOfWork.GetRepository<Metadata>();
            usersRepo = unitOfWork.GetRepository<User>();
            phaseRepo = unitOfWork.GetRepository<Phase>();
            categoryRepo = unitOfWork.GetRepository<AssetCategory>();

            CurrentUser = MainViewModel.Instance.CurrentUser;

            myAsset = new Asset();
            Creating = true;
            Cloning = false;
            SaveCommand = new RelayCommand((s) => Save(), (s) => true);
            RefuseSave = new RelayCommand((s) => navCoordinator.NavigateToQueued(), (s) => true);
            DeleteConfirmed = new RelayCommand((s) => DeleteAsset(), (s) => true);

            NotifyPropertyChanged("Users");
            NotifyPropertyChanged("Categories");
            NotifyPropertyChanged("Phases");
        }

        public void SetAsset(Asset ast)
        {
            myAsset = assetRepo.GetByID(ast.ID);
            Changelog = (from s in changesRepo.Get()
                         where s.ch_recid == myAsset.ID
                         orderby s.ch_datetime descending
                         select s).ToList();
            Creating = false;
            Cloning = ast.ID == 0;

            NotifyPropertyChanged("myAsset");
            NotifyPropertyChanged("Changelog");
            NotifyPropertyChanged("DiscussionBoard");
            NotifyPropertyChanged("myAsset");
            NotifyPropertyChanged("Tags");
            NotifyPropertyChanged("Users");
            NotifyPropertyChanged("Categories");
            NotifyPropertyChanged("Phases");
            SetHierarchy();
        }

        public void DeleteAsset()
        {
            assetDeleteStrategy.Delete(unitOfWork, myAsset);
            unitOfWork.Commit();
            navCoordinator.NavigateToAssetList();
        }

        #region SetNewValuesFromSearchboxes
        public void ModifyAssignedUser(int userID)
        {
            User user = usersRepo.GetByID(userID);
            if (user != null)
            {               
                myAsset.AssignedToUser = user;
                usersRepo.Update(user);
                NotifyPropertyChanged("IsSavable");
                NotifyPropertyChanged("myAsset");
            }
        }

        public void ModifyPhase(int phaseID)
        {
            Phase phase = phaseRepo.GetByID(phaseID);
            if (phase != null)
            {              
                myAsset.Phase = phase;
                assetRepo.Update(myAsset);
                NotifyPropertyChanged("IsSavable");
                NotifyPropertyChanged("myAsset");
            }
        }

        public void ModifyCategory(int catID)
        {
            AssetCategory cat = categoryRepo.GetByID(catID);
            if (cat != null)
            {
                Category = cat;
            }
        }
        #endregion

        public void Save()
        {
            assetRepo.Update(myAsset);

            if (myAsset.IsValid(out List<Violation> violations))
            {
                Asset beforeAsset = assetRepo.GetDBValues(myAsset);
                List<Change> changesToSave = myAsset.GetChanges(beforeAsset, CurrentUser);
                List<Alert> alertsToSave = myAsset.GetAlerts(beforeAsset, CurrentUser);

                changesRepo.Insert(changesToSave);
                alertsRepo.Insert(alertsToSave);
                unitOfWork.Commit();

                if (navCoordinator.WaitingToNavigate)
                {
                    navCoordinator.NavigateToQueued();
                }
                else
                {
                    NotifyPropertyChanged("IsSavable");
                    NotifyPropertyChanged("Changelog");
                    NotifyPropertyChanged("PhaseTimelineObjects");
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
            Metadata data = tagsRepo.GetByID(id);
            tagDeleteStrategy.Delete(unitOfWork, data);
            unitOfWork.Commit();

            NotifyPropertyChanged("IsSavable");
            NotifyPropertyChanged("Tags");
        }

        public void AddMetadata(string text)
        {
            Metadata dataInst = new Metadata();
            dataInst.md_asid = myAsset.as_id;
            dataInst.md_value = text;
            myAsset.Metadata.Add(dataInst);
            assetRepo.Update(myAsset);
            tagsRepo.Insert(dataInst);
            NotifyPropertyChanged("IsSavable");
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
            if (id != myAsset.ID)
            {
                Asset newAsset = assetRepo.GetByID(id);
                SetAsset(newAsset);
            }
        }

        #endregion

        #region PhaseTimeline
        public struct PhaseTimelineObject
        {
            public string PhaseName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string UserName { get; set; }

            public bool IsCompleted => EndDate != null && EndDate <= DateTime.Now;
            public bool IsInWork => !IsCompleted && (StartDate != null && StartDate > DateTime.MinValue);
            public string UserNameLabel => UserName == "" ? "Unassigned" : UserName;

            public string UserLabel
            {
                get
                {
                    if (IsCompleted)
                    {
                        return "Completed By:";
                    }
                    else if (IsInWork)
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

            public string DateLabel
            {
                get
                {
                    if (IsCompleted)
                    {
                        return StartDate.ToString("MM/dd") + "-" + EndDate.ToString("MM/dd");
                    }
                    else if (IsInWork)
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
        public List<PhaseTimelineObject> PhaseTimelineObjects
        {
            get
            {
                var phaseTimelineObjects = new List<PhaseTimelineObject>();
                List<Phase> phases = (from p in phaseRepo.Get()
                                      where p.ph_caid == myAsset.as_caid
                                      orderby p.ph_step ascending
                                      select p).ToList();

                if (phases.Count > 0)
                {
                    List<Change> phaseChanges = (from c in changesRepo.Get()
                                                 where c.ch_recid == myAsset.as_id
                                                 && (c.ch_description == ChangeType.CreatedAsset
                                                 || c.ch_description == ChangeType.ChangedPhase
                                                 || c.ch_description == ChangeType.UserAssigned)
                                                 select c).ToList();

                    int currentPhase = myAsset.Phase?.ph_step ?? 0;
                    foreach (Phase phase in phases)
                    {
                        DateTime startDate = DateTime.MinValue;
                        DateTime endDate = DateTime.MaxValue;
                        string username = "";

                        if (currentPhase >= phase.ph_step) // We need to account for steps backwards.
                        {
                            Change phaseChange = phaseChanges.FirstOrDefault(c => c.ch_description == ChangeType.ChangedPhase &&
                                                                             c.ch_newvalue == (phase.ph_id).ToString());
                            // Get start date through creation date if this is the first phase OR
                            // when the phase was changed from the previous step
                            if (phase.ph_step == 1 && phaseChange == null)
                            {
                                Change createChange = phaseChanges.FirstOrDefault(c => c.ch_description == ChangeType.CreatedAsset);
                                startDate = createChange?.ch_datetime ?? DateTime.MinValue;
                            }

                            if (phaseChange != null)
                            {
                                startDate = phaseChange.ch_datetime;
                            }


                            Change phaseEndChange = phaseChanges.FirstOrDefault(c => c.ch_description == ChangeType.ChangedPhase &&
                                                                           c.ch_oldvalue == (phase.ph_id).ToString());
                            if (phaseEndChange != null)
                            {
                                endDate = phaseEndChange.ch_datetime;
                            }

                            if (phase.ph_step <= currentPhase)
                            {
                                if (phaseChanges.Any(c => c.ch_description == ChangeType.UserAssigned))
                                {
                                    List<Change> userAssignsInPeriod = phaseChanges.Where(c => c.ch_description == ChangeType.UserAssigned &&
                                                                                      c.ch_datetime <= endDate)
                                                                                      .OrderByDescending(c => c.ch_datetime).ToList();
                                    if (userAssignsInPeriod.Count > 0)
                                    {
                                        Change userAssignChange = userAssignsInPeriod.FirstOrDefault();
                                        int userID = int.Parse(userAssignChange.ch_newvalue);
                                        User userassigned = usersRepo.GetByID(userID);
                                        username = userassigned.Name;
                                    }
                                }
                            }
                        }

                        PhaseTimelineObject timelineObject = new PhaseTimelineObject()
                        {
                            StartDate = startDate,
                            EndDate = endDate,
                            PhaseName = phase.ph_name,
                            UserName = username
                        };

                        phaseTimelineObjects.Add(timelineObject);
                    }
                }
                return phaseTimelineObjects;

            }
        }


        #endregion

        public void CreateNewDiscussio(object input)
        {
            string defaultText = "Start a new discussion..";
            object[] values = input as object[];
            int parentID = (int)values[0];
            string content = values[1] as string;
            if (content != defaultText && content != "")
            {
                Discussion newDiscussion = new Discussion();
                newDiscussion.di_contents = content;
                newDiscussion.di_date = DateTime.Now;
                newDiscussion.di_asid = myAsset.ID;
                newDiscussion.di_usid = MainViewModel.Instance.CurrentUser.us_id;
                if (parentID > 0)
                {
                    newDiscussion.di_parentid = parentID;

                    Discussion parentDiscussion = discussionRepo.GetByID(parentID);
                    int parentUserId = parentDiscussion.di_usid ?? 0;
                    if (parentUserId > 0 &&
                        parentUserId != MainViewModel.Instance.CurrentUser.us_id)
                    {
                        //TODO: Create an alert for all users that have replied on the discussion
                        User parentUser = usersRepo.GetByID(parentUserId);
                        Alert newAlert = new Alert();
                        newAlert.ar_usid = parentDiscussion.di_usid;
                        newAlert.ar_projectwide = false;
                        newAlert.ar_asid = myAsset.as_id;
                        newAlert.ar_date = DateTime.Now;
                        newAlert.ar_type = AlertType.DiscussionReply;
                        newAlert.ar_header = parentUser.us_displayname + " continued the conversation on #" + myAsset.as_id;
                        newAlert.ar_content = content.Substring(0, Math.Min(content.Length, 47)) + "...";

                        alertsRepo.Insert(newAlert);
                    }
                }

                discussionRepo.Insert(newDiscussion);
                unitOfWork.Commit();
                NotifyPropertyChanged("DiscussionBoard");
            }
        }

    }
}
