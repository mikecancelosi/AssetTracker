using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.View.Services;
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
        #region ModelAccessors
        public Asset myAsset { get; private set; }
        public List<Metadata> Tags => myAsset.Metadata.Count > 0 ? myAsset.Metadata.ToList() : null;
        public List<Change> Changelog { get; set; }
        public List<AssetHierachyObjectBuilder.AssetHierarchyObject> Hierarchy => AssetHierachyObjectBuilder.CreateHierarchy(myAsset);
        private Asset QueuedHierarchyObject;
        
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
        #endregion

        #region ViewState
        /// <summary>
        /// Is myAsset a new asset we are generating?
        /// </summary>
        private bool Creating;
        /// <summary>
        /// Are we just copying a prexisting asset
        /// </summary>
        private bool Cloning;

        public Violation DescriptionViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "as_description") ?? null;
        public Violation NameViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "as_displayname") ?? null;

        #endregion

        #region ISavable Properties
        /// <summary>
        /// If the user has made changes we can save them
        /// </summary>
        public bool IsSavable => unitOfWork.HasChanges;
        /// <summary>
        /// When the user has determined to save, this command should execute save
        /// </summary>
        public ICommand SaveCommand => new RelayCommand((s) => Save(), (s) => true);
        /// <summary>
        /// When user rejects a save after selecting to navigate away, we just ignore changes and continue navigation
        /// </summary>
        public ICommand RefuseSave => new RelayCommand((s) => SaveRefused(), (s) => true);
        /// <summary>
        /// Denotes any violations found when attempting to save.
        /// </summary>
        public List<Violation> SaveViolations { get; set; }
        /// <summary>
        /// Nav coordinator to use to navigate away from the current view
        /// </summary>
        public INavigationCoordinator navCoordinator { get; set; }

        /// <summary>
        /// Should the user be prompted to save
        /// </summary>
        private bool promptSave;
        /// <summary>
        /// Should the user be prompted to save
        /// </summary>
        public bool PromptSave
        {
            get => promptSave;
            set
            {
                promptSave = value;
                NotifyPropertyChanged("PromptSave");
            }
        }
        #endregion

        #region Delete Properties    
        private bool promptDelete;
        public bool PromptDelete
        {
            get => promptDelete;
            set
            {
                promptDelete = value;
                NotifyPropertyChanged("PromptDelete");
            }
        }
        /// <summary>
        /// The user has selected to delete, we need them to confirm before deleting.
        /// </summary>
        public ICommand PromptDeleteCommand => new RelayCommand((s) => PromptDelete = true, (s) => true);
        /// <summary>
        /// Deletion was confirmed, progress to delete.
        /// </summary>
        public ICommand DeleteConfirmed => new RelayCommand((s) => DeleteAsset(), (s) => true);
        /// <summary>
        /// Deletion was rejected, hide the prompt
        /// </summary>
        public ICommand DeleteCancelled => new RelayCommand((s) =>  PromptDelete = false, (s) => true);
        /// <summary>
        /// Strategy to handle deleting the asset
        /// </summary>
        private IDeleteStrategy<Asset> assetDeleteStrategy;
        /// <summary>
        /// Strategy to handle deleting a tag.
        /// </summary>
        private IDeleteStrategy<Metadata> tagDeleteStrategy;

        #endregion

        #region Repositories
        private GenericRepository<Asset> assetRepo;
        private GenericRepository<Discussion> discussionRepo;
        private GenericRepository<Change> changesRepo;
        private GenericRepository<Alert> alertsRepo;
        private GenericRepository<Metadata> tagsRepo;
        private GenericRepository<User> usersRepo;
        private GenericRepository<Phase> phaseRepo;
        private GenericRepository<AssetCategory> categoryRepo;
        #endregion

        /// <summary>
        /// User currently logged in
        /// </summary>
        private User CurrentUser;

        /// <summary>
        /// Command when a discussion reply, or a new discussion was made.
        /// </summary>
        public ICommand DiscussionReplyClicked => new RelayCommand((arr) => CreateNewDiscussio(arr), (arr) => { return true; });

        #region Permissions
        public bool UserCanDeleteAssets => PermissionsManager.HasPermission(CurrentUser, Permission.DeleteAsset, unitOfWork);
        #endregion

        public AssetDetailViewModel(INavigationCoordinator coord, GenericUnitOfWork uow, IDeleteStrategy<Asset> assetDeleteStrat, IDeleteStrategy<Metadata> tagDeleteStrat)
        {
            navCoordinator = coord;
            assetDeleteStrategy = assetDeleteStrat;
            tagDeleteStrategy = tagDeleteStrat;
            unitOfWork = uow;

            navCoordinator.UserNavigationAttempt += (s) => PromptSave = true;

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

            NotifyPropertyChanged("Users");
            NotifyPropertyChanged("Categories");
            NotifyPropertyChanged("Phases");
        }

        /// <summary>
        /// Change the asset this viewmodel is bound to
        /// </summary>
        /// <param name="ast">Asset to set the viewmodel to</param>
        public void SetAsset(Asset ast)
        {
            myAsset = assetRepo.GetByID(ast.ID);
            Changelog = (from s in changesRepo.Get()
                         where s.ch_recid == myAsset.ID
                         orderby s.ch_datetime descending
                         select s).ToList();
            Creating = false;
            Cloning = ast.ID == 0;
            unitOfWork.Rollback();

            NotifyPropertyChanged("myAsset");
            NotifyPropertyChanged("Changelog");
            NotifyPropertyChanged("DiscussionBoard");
            NotifyPropertyChanged("AssetTitle");
            NotifyPropertyChanged("Tags");
            NotifyPropertyChanged("Users");
            NotifyPropertyChanged("Categories");
            NotifyPropertyChanged("Phases");
            NotifyPropertyChanged("Hierarchy");
            NotifyPropertyChanged("IsSavable");
            NotifyPropertyChanged("PhaseTimelineObjects");
        }

        /// <summary>
        /// Delete this asset and navigate back to the asset list
        /// </summary>
        public void DeleteAsset()
        {
            assetDeleteStrategy.Delete(unitOfWork, myAsset);
            unitOfWork.Commit();
            navCoordinator.NavigateToAssetList();
        }       

        /// <summary>
        /// Save this asset, if valid
        /// </summary>
        public void Save()
        {
            PromptSave = false;
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
                    SaveViolations = new List<Violation>();
                    NotifyPropertyChanged("SaveViolations");
                    NotifyPropertyChanged("DescriptionViolation");
                    NotifyPropertyChanged("NameViolation");
                    NotifyPropertyChanged("IsSavable");
                    NotifyPropertyChanged("Changelog");
                    NotifyPropertyChanged("PhaseTimelineObjects");

                    if(QueuedHierarchyObject != null)
                    {
                        SetAsset(QueuedHierarchyObject);
                        QueuedHierarchyObject = null;
                    }
                }
            }
            else
            {
                SaveViolations = violations;
                NotifyPropertyChanged("SaveViolations");
                NotifyPropertyChanged("DescriptionViolation");
                NotifyPropertyChanged("NameViolation");
                
            }
        }

        /// <summary>
        /// When the user has been prompted to save and refuses, we either want to navigate away or change the asset
        /// </summary>
        private void SaveRefused()
        {
            PromptSave = false;

            if (QueuedHierarchyObject != null)
            {
                SetAsset(QueuedHierarchyObject);
                QueuedHierarchyObject = null;
            }
            else
            {
                navCoordinator.NavigateToQueued();
            }
        }

        #region Tags
        /// <summary>
        /// Delete the selected tag
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTag(int id)
        {
            Metadata data = tagsRepo.GetByID(id);
            tagDeleteStrategy.Delete(unitOfWork, data);
            unitOfWork.Commit();

            NotifyPropertyChanged("IsSavable");
            NotifyPropertyChanged("Tags");
        }

        /// <summary>
        /// Add new tag with given text to this asset
        /// </summary>
        /// <param name="text">Text to set the new tag to use</param>
        public void AddTag(string text)
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
        
        /// <summary>
        /// When a hierarchy object is selected, we want to change the current asset to the new one.
        /// </summary>
        /// <param name="id">ID of the selected asset.</param>
        public void OnHierarchyAssetSelected(int id)
        {       
            if (id != myAsset.ID)
            {
                Asset newAsset = assetRepo.GetByID(id);
                if (IsSavable)
                {
                    PromptSave = true;
                    QueuedHierarchyObject = newAsset;
                }
                else
                {                   
                    SetAsset(newAsset);
                }
            }
        }

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

        /// <summary>
        /// We are either creating a brand new dicussion or replying to an existing one
        /// </summary>
        /// <param name="input">array object holding parent ID and discussion content</param>
        public void CreateNewDiscussio(object input)
        {
            object[] values = input as object[];
            int parentID = (int)values[0];
            string content = values[1] as string;

            Discussion newDiscussion = new Discussion()
            {
                di_contents = content,
                di_date = DateTime.Now,
                di_asid = myAsset.ID,
                di_usid = CurrentUser.us_id,
                di_parentid = parentID
            };
            //If we are adding this to a prexisting discussion ,we need to alert all other users to new content
            if (newDiscussion.di_parentid > 0)
            {                
                Discussion parentDiscussion = discussionRepo.GetByID(parentID);
                List<Discussion> otherDiscussions = (from d in discussionRepo.Get()
                                                     where d.di_parentid == parentDiscussion.di_id
                                                     select d).ToList();
                otherDiscussions.Add(parentDiscussion);
                List<Alert> alertsToPost = newDiscussion.GetAlerts(otherDiscussions);
                foreach(Alert al in alertsToPost)
                {
                    alertsRepo.Insert(al);
                }
            }

            discussionRepo.Insert(newDiscussion);
            unitOfWork.Commit();
            NotifyPropertyChanged("DiscussionBoard");

        }

    }
}
