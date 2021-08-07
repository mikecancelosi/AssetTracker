using AssetTracker.Services;
using AssetTracker.View.Commands;
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
    public class ProjectSettingsViewModel : ViewModel
    {
        public ObservableCollection<User> Users => new ObservableCollection<User>(userRepo.Get());
        public ObservableCollection<SecRole> Roles => new ObservableCollection<SecRole>(roleRepo.Get());
        public ObservableCollection<AssetCategory> Categories => new ObservableCollection<AssetCategory>(categoryRepo.Get());

        /// <summary>
        /// These are alerts that are visible to everyone. Typically messages
        /// like 'Remember to keep your assets updated with the current status'
        /// </summary>
        public List<Alert> ProjectWideAlerts
        {
            get
            {
                return (from a in alertRepo.Get()
                        where a.ar_usid != null &&
                        a.ar_projectwide
                        select a).ToList();
            }
        }

        #region Commands
        public ICommand CreateRole { get; set; }
        public ICommand CreateCategory { get; set; }
        public ICommand CreateUser { get; set; }
        public ICommand CreateAlert => new RelayCommand((s) => CreateNewProjectWideAlert(), (s) => true);
        public ICommand OpenNewAlert => new RelayCommand((s) => PromptNewAlert = true, (s) => true);
        public ICommand CloseNewAlert => new RelayCommand((s) => PromptNewAlert = false, (s) => true);
        public ICommand EditAlertCommand => new RelayCommand((s) => EditAlert(s), (s) => true);
        public ICommand DeleteAlertCommand => new RelayCommand((s) => DeleteAlert(s), (s) => true);
        #endregion

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
        public ICommand DeleteConfirmed { get; set; }
        public ICommand DeleteCanceled { get; set; }
        public DatabaseBackedObject DeletionObject { get; private set; }

        public enum OperationType
        {
            Copy,
            Edit,
            Delete
        }

        #region Repositories
        private GenericRepository<User> userRepo;
        private GenericRepository<SecRole> roleRepo;
        private GenericRepository<AssetCategory> categoryRepo;
        private GenericRepository<Alert> alertRepo;
        private GenericRepository<SecPermission3> roleOverrideRepo;
        #endregion

        #region DeleteStrategies
        private IDeleteStrategy<SecRole> secRoleDeleteStrategy;
        private IDeleteStrategy<Alert> alertDeleteStrategy;
        private IDeleteStrategy<AssetCategory> catDeleteStrategy;
        private IDeleteStrategy<User> userDeleteStrategy;
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


        private User LoggedInUser;

        private readonly INavigationCoordinator navCoordinator;
        public ProjectSettingsViewModel(INavigationCoordinator coord, 
                                        GenericUnitOfWork uow,
                                        User loggedInUser,
                                        IDeleteStrategy<SecRole> roleDeleteStrat,
                                        IDeleteStrategy<Alert> alertDeleteStrat,
                                        IDeleteStrategy<User> userDeleteStrat,
                                        IDeleteStrategy<AssetCategory> catDeleteStrat)
        {
            navCoordinator = coord;
            unitOfWork = uow;
            LoggedInUser = loggedInUser;

            userRepo = unitOfWork.GetRepository<User>();
            roleRepo = unitOfWork.GetRepository<SecRole>();
            categoryRepo = unitOfWork.GetRepository<AssetCategory>();
            alertRepo = unitOfWork.GetRepository<Alert>();
            roleOverrideRepo = unitOfWork.GetRepository<SecPermission3>();

            secRoleDeleteStrategy = roleDeleteStrat;
            alertDeleteStrategy = alertDeleteStrat;
            catDeleteStrategy = catDeleteStrat;
            userDeleteStrategy = userDeleteStrat;

            CreateRole = new RelayCommand((s) => navCoordinator.NavigateToCreateRole(), (s) => true);
            CreateCategory = new RelayCommand((s) => navCoordinator.NavigateToCreateCategory(), (s) => true);
            CreateUser = new RelayCommand((s) => navCoordinator.NavigateToCreateUser(), (s) => true);
            DeleteConfirmed = new RelayCommand((s) => DeleteSelectedObject(), (s) => true);
            DeleteCanceled = new RelayCommand((s) => { PromptDelete = false; }, (s) => true);
        }

        public void CompleteDBOOperation(DatabaseBackedObject dbo, OperationType operation)
        {
            //TODO : Fix this monstrosity
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

        public void Reload()
        {
            unitOfWork.Rollback();
            NotifyPropertyChanged("Users");
            NotifyPropertyChanged("Roles");
            NotifyPropertyChanged("Categories");
        }

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
                    var roleItem =(SecRole)DeletionObject;
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

        private void EditAlert(object input)
        {
            int id = (int)input;
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

        private void DeleteAlert(object input)
        {
            int id = (int)input;
            Alert alert = ProjectWideAlerts.FirstOrDefault(x => x.ID == id);
            CompleteDBOOperation(alert, OperationType.Delete);
        }
    }
}
