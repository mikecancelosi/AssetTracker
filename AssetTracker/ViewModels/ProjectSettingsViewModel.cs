using AssetTracker.Services;
using AssetTracker.View.Commands;
using DataAccessLayer;
using DataAccessLayer.Services;
using DataAccessLayer.Strategies;
using DomainModel;
using DomainModel.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AssetTracker.ViewModels
{
    public class ProjectSettingsViewModel : ViewModel
    {
        #region List Values
        /// <summary>
        /// Object that the user list is bound to
        /// </summary>
        public ObservableCollection<User> Users => new ObservableCollection<User>(userRepo.Get());
        /// <summary>
        /// Object that the role list is bound to
        /// </summary>
        public ObservableCollection<SecRole> Roles => new ObservableCollection<SecRole>(roleRepo.Get());
        /// <summary>
        /// Object that the category list is bound to
        /// </summary>
        public ObservableCollection<AssetCategory> Categories => new ObservableCollection<AssetCategory>(categoryRepo.Get());

        /// <summary>
        /// These are alerts that are visible to everyone. Typically messages
        /// like 'Remember to keep your assets updated with the current status'
        /// </summary>
        public ObservableCollection<Alert> ProjectWideAlerts
        {
            get
            {
                var listResults = (from a in alertRepo.Get()
                                   where a.ar_usid != null &&
                                   a.ar_projectwide
                                   select a).ToList();

                return new ObservableCollection<Alert>(listResults);
            }
        }
        #endregion 

        #region List Commands
        /// <summary>
        /// 'Add' button was selected, we want to navigate to the create user page
        /// </summary>
        public ICommand CreateUser => new RelayCommand((s) => navCoordinator.NavigateToCreateUser(), (s) => true);
        /// <summary>
        /// 'Copy' button was selected, we want to copy the SelectedUser and then navigate to the clone user page
        /// </summary>
        public ICommand CopySelectedUser => new RelayCommand((s) => CompleteDBOOperation(SelectedUser, OperationType.Copy), (s) => true);
        /// <summary>
        /// 'Edit' button was selected, we want to modify the SelectedUser at the EditUser page
        /// </summary>
        public ICommand EditSelectedUser => new RelayCommand((s) => CompleteDBOOperation(SelectedUser, OperationType.Edit), (s) => true);
        /// <summary>
        /// 'Delete button was selected, we want to delete the user using our delete user strategy
        /// </summary>
        public ICommand DeleteSelectedUser => new RelayCommand((s) => CompleteDBOOperation(SelectedUser, OperationType.Delete), (s) => true);

        /// <summary>
        /// 'Add' button was selected, we want to navigate to the create role page
        /// </summary>
        public ICommand CreateRole => new RelayCommand((s) => navCoordinator.NavigateToCreateRole(), (s) => true);
        /// <summary>
        /// 'Copy' button was selected, we want to copy the SelectedRole and then navigate to the clone role page
        /// </summary>
        public ICommand CopySelectedRole => new RelayCommand((s) => CompleteDBOOperation(SelectedRole, OperationType.Copy), (s) => true);
        /// <summary>
        /// 'Edit' button was selected, we want to modify the SelectedRole at the EditRole page
        /// </summary>
        public ICommand EditSelectedRole => new RelayCommand((s) => CompleteDBOOperation(SelectedRole, OperationType.Edit), (s) => true);
        /// <summary>
        /// 'Delete button was selected, we want to delete the SelectedRole using our delete role strategy
        /// </summary>
        public ICommand DeleteSelectedRole => new RelayCommand((s) => CompleteDBOOperation(SelectedRole, OperationType.Delete), (s) => true);

        /// <summary>
        /// 'Add' button was selected, we want to navigate to the create category page
        /// </summary>
        public ICommand CreateCategory => new RelayCommand((s) => navCoordinator.NavigateToCreateCategory(), (s) => true);
        /// <summary>
        /// 'Copy' button was selected, we want to copy the SelectedCategory and then navigate to the clone category page
        /// </summary>
        public ICommand CopySelectedCategory => new RelayCommand((s) => CompleteDBOOperation(SelectedCategory, OperationType.Copy), (s) => true);
        /// <summary>
        /// 'Edit' button was selected, we want to modify the SelectedCategory at the EditCategory page
        /// </summary>
        public ICommand EditSelectedCategory => new RelayCommand((s) => CompleteDBOOperation(SelectedCategory, OperationType.Edit), (s) => true);
        /// <summary>
        /// 'Delete button was selected, we want to delete the SelectedCategory using our delete category strategy
        /// </summary>
        public ICommand DeleteSelectedCategory => new RelayCommand((s) => CompleteDBOOperation(SelectedCategory, OperationType.Delete), (s) => true);
        
        /// <summary>
        /// Create alert button was selected, we want to open up the popup to allow user to create a new alert
        /// </summary>
        public ICommand OpenNewAlert => new RelayCommand((s) => PromptNewAlert = true, (s) => true);
        /// <summary>
        /// Creation of a new alert was cancelled, just close the creation popup
        /// </summary>
        public ICommand CloseNewAlert => new RelayCommand((s) => PromptNewAlert = false, (s) => true);
        /// <summary>
        /// Creation of a new alert was confirmed
        /// </summary>
        public ICommand CreateAlert => new RelayCommand((s) => CreateNewProjectWideAlert(), (s) => true);
        /// <summary>
        /// User wants to edit existing alert
        /// </summary>
        public ICommand EditAlertCommand => new RelayCommand((s) => EditAlert(s), (s) => true);
        /// <summary>
        /// User wants to delete an alert.
        /// </summary>
        public ICommand DeleteAlertCommand => new RelayCommand((s) => DeleteAlert(s), (s) => true);
        #endregion

        #region List Current Selections
        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                NotifyPropertyChanged("SelectedUser");
            }
        }
        private SecRole selectedRole;
        public SecRole SelectedRole
        {
            get => selectedRole;
            set
            {
                selectedRole = value;
                NotifyPropertyChanged("SelectedRole");
            }
        }
        private AssetCategory selectedCategory;
        public AssetCategory SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                NotifyPropertyChanged("SelectedCategory");
            }
        }
        #endregion

        #region Alert Properties
        private bool promptNewAlert;
        public bool PromptNewAlert
        {
            get => promptNewAlert;
            set
            {
                if (value == false)
                {
                    AlertPrompt_Header = "";
                    AlertPrompt_Contents = "";
                    NotifyPropertyChanged("NewPrompt_Header");
                    NotifyPropertyChanged("NewPrompt_Contents");
                }
                promptNewAlert = value;
                NotifyPropertyChanged("PromptNewAlert");
            }
        }

        public string AlertPrompt_Header { get; set; }
        public string AlertPrompt_Contents { get; set; }
        public int AlertPrompt_ID { get; set; }
        #endregion

        #region Deleting
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
        public ICommand DeleteConfirmed => new RelayCommand((s) => DeleteSelectedObject(), (s) => true);
        public ICommand DeleteCanceled => new RelayCommand((s) => { PromptDelete = false; }, (s) => true);
        public DatabaseBackedObject DeletionObject { get; private set; }

        private IDeleteStrategy<SecRole> secRoleDeleteStrategy;
        private IDeleteStrategy<Alert> alertDeleteStrategy;
        private IDeleteStrategy<AssetCategory> catDeleteStrategy;
        private IDeleteStrategy<User> userDeleteStrategy;
        #endregion

        public enum OperationType
        {
            Copy,
            Edit,
            Delete
        }

        #region Repositories
        private IRepository<User> userRepo;
        private IRepository<SecRole> roleRepo;
        private IRepository<AssetCategory> categoryRepo;
        private IRepository<Alert> alertRepo;
        private IRepository<SecPermission3> roleOverrideRepo;
        #endregion
              
        #region Permissions
        public bool UserCanCreateUsers => PermissionsManager.HasPermission(LoggedInUser, Permission.CreateUser, unitOfWork);
        public bool UserCanDeleteUsers => PermissionsManager.HasPermission(LoggedInUser, Permission.DeleteUser, unitOfWork);
        public bool UserCanEditUsers => PermissionsManager.HasPermission(LoggedInUser, Permission.EditUser, unitOfWork);
        public bool UserCanCloneUsers => PermissionsManager.HasPermission(LoggedInUser, Permission.CloneUser, unitOfWork);
        public bool UserCanCreateRoles => PermissionsManager.HasPermission(LoggedInUser, Permission.CreateUserRole, unitOfWork);
        public bool UserCanDeleteRoles => PermissionsManager.HasPermission(LoggedInUser, Permission.DeleteUserRole, unitOfWork);
        public bool UserCanEditRoles => PermissionsManager.HasPermission(LoggedInUser, Permission.ModifyUserRole, unitOfWork);
        public bool UserCanCloneRoles => PermissionsManager.HasPermission(LoggedInUser, Permission.CloneUserRole, unitOfWork);
        public bool UserCanCreateCategories => PermissionsManager.HasPermission(LoggedInUser, Permission.CreateAssetCategory, unitOfWork);
        public bool UserCanDeleteCategories => PermissionsManager.HasPermission(LoggedInUser, Permission.DeleteAssetCategory, unitOfWork);
        public bool UserCanEditCategories => PermissionsManager.HasPermission(LoggedInUser, Permission.ModifyAssetCategory, unitOfWork);
        public bool UserCanCloneCategories => PermissionsManager.HasPermission(LoggedInUser, Permission.CloneAssetCategory, unitOfWork);
        public bool UserCanPostAlerts => PermissionsManager.HasPermission(LoggedInUser, Permission.CreateAlert, unitOfWork);
        public bool UserCanEditAlerts => PermissionsManager.HasPermission(LoggedInUser, Permission.ModifyAlert, unitOfWork);
        public bool UserCanDeleteAlerts => PermissionsManager.HasPermission(LoggedInUser, Permission.DeleteAlert, unitOfWork);
        #endregion

        /// <summary>
        /// User currently logged into the system.
        /// </summary>
        private User LoggedInUser;
        /// <summary>
        /// Nav coordinator used to navigate away to other pages.
        /// </summary>
        private readonly INavigationCoordinator navCoordinator;

        public ProjectSettingsViewModel(INavigationCoordinator coord,
                                        IUnitOfWork uow,
                                        User loggedInUser,
                                        IDeleteStrategy<SecRole> roleDeleteStrat,
                                        IDeleteStrategy<Alert> alertDeleteStrat,
                                        IDeleteStrategy<User> userDeleteStrat,
                                        IDeleteStrategy<AssetCategory> catDeleteStrat)
        {
            navCoordinator = coord;
            unitOfWork = uow;
            LoggedInUser = loggedInUser;
            secRoleDeleteStrategy = roleDeleteStrat;
            alertDeleteStrategy = alertDeleteStrat;
            catDeleteStrategy = catDeleteStrat;
            userDeleteStrategy = userDeleteStrat;

            userRepo = unitOfWork.GetRepository<User>();
            roleRepo = unitOfWork.GetRepository<SecRole>();
            categoryRepo = unitOfWork.GetRepository<AssetCategory>();
            alertRepo = unitOfWork.GetRepository<Alert>();
            roleOverrideRepo = unitOfWork.GetRepository<SecPermission3>();
        }

        /// <summary>
        /// Copy,Edit, or Delete the given dbo
        /// </summary>
        /// <param name="dbo"> object to perform crud operation on</param>
        /// <param name="operation">What kind of operation should be performed on the object</param>
        public void CompleteDBOOperation(DatabaseBackedObject dbo, OperationType operation)
        {
            //TODO : Fix this monstrosity -- ASP 
            string typeName = dbo.GetType().BaseType.Name;
            switch (operation)
            {

                case OperationType.Copy:
                    DatabaseBackedObject clone = dbo.Clone();
                    if (typeName == "SecRole")
                    {
                        navCoordinator.NavigateToRoleEdit((SecRole)clone);
                    }
                    else if (typeName == "AssetCategory")
                    {
                        navCoordinator.NavigatetoCategoryEdit((AssetCategory)clone);
                    }
                    else if (typeName == "User")
                    {
                        navCoordinator.NavigateToUserEdit((User)clone);
                    }
                    break;
                case OperationType.Edit:
                    if (typeName == "SecRole")
                    {
                        navCoordinator.NavigateToRoleEdit((SecRole)dbo);
                    }
                    else if (typeName == "AssetCategory")
                    {
                        navCoordinator.NavigatetoCategoryEdit((AssetCategory)dbo);
                    }
                    else if (typeName == "User")
                    {
                        navCoordinator.NavigateToUserEdit((User)dbo);
                    }
                    break;
                case OperationType.Delete:
                    DeletionObject = dbo;
                    PromptDelete = true;
                    NotifyPropertyChanged("DeletionObject");
                    break;
            }
        }

        /// <summary>
        /// After deletion is confirmed, actually go through with the deleting the current object
        /// </summary>
        private void DeleteSelectedObject()
        {
            switch (DeletionObject.GetType().BaseType.Name)
            {
                case "User":
                    var userItem = (User)DeletionObject;
                    userDeleteStrategy.Delete(unitOfWork, userItem);
                    unitOfWork.Commit();
                    NotifyPropertyChanged("Users");
                    break;
                case "SecRole":
                    var roleItem = (SecRole)DeletionObject;
                    secRoleDeleteStrategy.Delete(unitOfWork, roleItem);
                    unitOfWork.Commit();
                    NotifyPropertyChanged("Roles");
                    break;
                case "AssetCategory":
                    var catItem = (AssetCategory)DeletionObject;
                    catDeleteStrategy.Delete(unitOfWork, catItem);
                    unitOfWork.Commit();
                    NotifyPropertyChanged("Categories");
                    break;
                case "Alert":
                    var alertItem = (Alert)DeletionObject;
                    alertDeleteStrategy.Delete(unitOfWork, alertItem);
                    unitOfWork.Commit();
                    NotifyPropertyChanged("ProjectWideAlerts");
                    break;
            }

            DeletionObject = null;
            PromptDelete = false;
        }

        /// <summary>
        /// If the user has filled in all required values in the alert prompt, then create the new object
        /// </summary>
        private void CreateNewProjectWideAlert()
        {
            Alert alert;
            if (AlertPrompt_ID > 0)
            {
                alert = ProjectWideAlerts.FirstOrDefault(x => x.ID == AlertPrompt_ID);
            }
            else
            {
                alert = new Alert()
                {
                    ar_projectwide = true,
                    ar_type = AlertType.ProjectWideAlert,
                    ar_priority = false
                };
                alertRepo.Insert(alert);
            }
            alert.ar_header = AlertPrompt_Header;
            alert.ar_content = AlertPrompt_Contents;
            alert.ar_date = DateTime.Now;
            alert.ar_usid = MainViewModel.Instance.CurrentUser.ID;
            unitOfWork.Commit();

            ResetAlertPrompt();
        }

        /// <summary>
        /// Reset visuals for creating new alert prompts 
        /// </summary>
        private void ResetAlertPrompt()
        {
            AlertPrompt_ID = 0;
            AlertPrompt_Contents = "";
            AlertPrompt_Header = "";
            PromptNewAlert = false;

            NotifyPropertyChanged("AlertPrompt_ID");
            NotifyPropertyChanged("AlertPrompt_Contents");
            NotifyPropertyChanged("AlertPrompt_Header");
            NotifyPropertyChanged("ProjectWideAlerts");
        }

        /// <summary>
        /// Edit alert by filling the editalert prompt with this alert's content
        /// </summary>
        /// <param name="alertID">alert id as object</param>
        private void EditAlert(object alertID)
        {
            int id = (int)alertID;
            Alert alert = ProjectWideAlerts.FirstOrDefault(x => x.ID == id);
            if (alert != null)
            {
                PromptNewAlert = true;
                AlertPrompt_Header = alert.ar_header;
                AlertPrompt_Contents = alert.ar_content;
                AlertPrompt_ID = alert.ID;
            }

            NotifyPropertyChanged("AlertPrompt_Header");
            NotifyPropertyChanged("AlertPrompt_Contents");
        }

        /// <summary>
        /// Delete alert with id matching input
        /// </summary>
        /// <param name="alertID">alert id as object</param>
        private void DeleteAlert(object alertID)
        {
            int id = (int)alertID;
            Alert alert = ProjectWideAlerts.FirstOrDefault(x => x.ID == id);
            CompleteDBOOperation(alert, OperationType.Delete);
        }
    }
}
