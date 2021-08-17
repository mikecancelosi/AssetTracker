using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.ViewModels.Interfaces;
using AssetTracker.ViewModels.Services;
using DataAccessLayer;
using DataAccessLayer.Services;
using DataAccessLayer.Strategies;
using DomainModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using static DomainModel.SecPermission;

namespace AssetTracker.ViewModels
{
    public class UserEditViewModel : ViewModel, ISavable
    {
        /// <summary>
        /// User to edit
        /// </summary>
        public User CurrentUser { get; private set; }
        /// <summary>
        /// Is the user new?
        /// </summary>
        public bool Creating { get; set; }
        /// <summary>
        /// Is the user a clone?
        /// </summary>
        private bool Cloning { get; set; }
        /// <summary>
        /// Display title based on whether we are modifying,cloning, or creating this user
        /// </summary>
        public string HeadingContext
        {
            get
            {
                if (Creating)
                {
                    return "Create User";
                }
                else if (Cloning)
                {
                    return "Clone User";
                }
                else
                {
                    return "Modify User";
                }
            }
        }

        /// <summary>
        /// First name of the user
        /// </summary>
        public string FirstName
        {
            get => CurrentUser?.us_fname ?? "";
            set
            {
                CurrentUser.us_fname = value;
                userRepo.Update(CurrentUser);
                NotifyPropertyChanged("FirstName");
                NotifyPropertyChanged("IsSavable");
            }
        }
        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName
        {
            get => CurrentUser?.us_lname ?? "";
            set
            {
                CurrentUser.us_lname = value;
                userRepo.Update(CurrentUser);
                NotifyPropertyChanged("LastName");
                NotifyPropertyChanged("IsSavable");
            }
        }
        /// <summary>
        /// Display name of the user
        /// </summary>
        public string DisplayName
        {
            get => CurrentUser?.us_displayname ?? "";
            set
            {
                CurrentUser.us_displayname = value;
                userRepo.Update(CurrentUser);
                NotifyPropertyChanged("DisplayName");
                NotifyPropertyChanged("IsSavable");
            }
        }
        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email
        {
            get => CurrentUser?.us_email ?? "";
            set
            {
                CurrentUser.us_email = value;
                userRepo.Update(CurrentUser);
                NotifyPropertyChanged("Email");
                NotifyPropertyChanged("IsSavable");
            }
        }
        /// <summary>
        /// Password of the user
        /// </summary>
        public string Password
        {
            get => CurrentUser?.us_password ?? "";
            set
            {
                CurrentUser.us_password = value;
                userRepo.Update(CurrentUser);
                NotifyPropertyChanged("Password");
                NotifyPropertyChanged("IsSavable");
            }
        }
        /// <summary>
        /// Role of the user
        /// </summary>
        public SecRole UserRole
        {
            get => CurrentUser.SecRole;
            set
            {
                CurrentUser.SecRole = value;
                CurrentUser.us_roid = value.ID;
                userRepo.Update(CurrentUser);
                NotifyPropertyChanged("CurrentUser");
                NotifyPropertyChanged("IsSavable");
            }
        }

        #region Saving
        /// <summary>
        /// Execute save functionality
        /// </summary>
        public ICommand SaveCommand => new RelayCommand((s) => Save(), (s) => true);
        /// <summary>
        /// After being prompted to save, the user has refused. Navigate away to queued page
        /// </summary>
        public ICommand RefuseSave => new RelayCommand((s) => navCoordinator.NavigateToQueued(), (s) => true);
        /// <summary>
        /// Does the uow have any changes to save to the db
        /// </summary>
        public bool IsSavable => unitOfWork.HasChanges;
        /// <summary>
        /// Save violations detected on attempting to save
        /// </summary>
        public List<Violation> SaveViolations { get; set; }
        /// <summary>
        /// Backing field for PromptSave
        /// </summary>
        private bool promptSave;
        /// <summary>
        /// Should the user be prompted to save.
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
        /// <summary>
        /// Validator to use when saving the user
        /// </summary>
        private IModelValidator<User> userValidator;
        #endregion

        /// <summary>
        /// Coordinator to use when navigating away
        /// </summary>
        public INavigationCoordinator navCoordinator { get; set; }

        #region Deleting
        private IDeleteStrategy<User> userDeleteStrategy;
        public ICommand DeleteConfirmed => new RelayCommand((s) => DeleteUser(), (s) => true);
        #endregion

        #region Permissions
        public List<PermissionGroup> PermissionGroups => permissionsProvider.PermissionGroups;
        public ICommand ResetAllPermissionsCommand => new RelayCommand((s) => ResetAllPermissions(), (s) => true);
        public ICommand ActivateAllPermissionsCommand => new RelayCommand((s) => ActivateAllPermissions(), (s) => true);
        public ICommand DeactivateAllPermissionsCommand => new RelayCommand((s) => DeactivateAllPermissions(), (s) => true);
        public ICommand PermissionChanged => new RelayCommand((s) => PermissionUpdated(), (s) => true);
        private UserPermissionsProvider permissionsProvider;
        #endregion


        #region Repositories
        private IRepository<User> userRepo;
        private IRepository<SecRole> roleRepo;
        #endregion

        #region ErrorChecks
        public Violation FirstNameViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "us_fname") ?? null;
        public Violation LastNameViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "us_lname") ?? null;
        public Violation DisplayNameViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "us_displayname") ?? null;
        public Violation EmailViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "us_email") ?? null;
        public Violation PasswordViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "us_password") ?? null;
        public Violation RoleUnassignedViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "us_roid") ?? null;
        #endregion

        /// <summary>
        /// Roles available to set the user to
        /// </summary>
        public List<DatabaseBackedObject> Roles
        {
            get
            {
                return (from r in roleRepo.Get()
                        select r).ToList<DatabaseBackedObject>();
            }
        }

        public UserEditViewModel(INavigationCoordinator coord, IUnitOfWork uow, IDeleteStrategy<User> userDeleteStrat, IModelValidator<User> userValidatorService)
        {
            navCoordinator = coord;
            userDeleteStrategy = userDeleteStrat;
            unitOfWork = uow;
            userValidator = userValidatorService;

            navCoordinator.UserNavigationAttempt += (s) => PromptSave = true;            
            userRepo = unitOfWork.GetRepository<User>();
            roleRepo = unitOfWork.GetRepository<SecRole>();

            CurrentUser = new User();
            Creating = true;
            Cloning = false;
            permissionsProvider = new UserPermissionsProvider(unitOfWork, CurrentUser);

            NotifyPropertyChanged("UserRole");
            NotifyPropertyChanged("Roles");

        }

        /// <summary>
        /// Set the user to modify
        /// </summary>
        /// <param name="user"></param>
        public void SetUser(User user)
        {
            if (user.us_id > 0) // Modifying
            {
                CurrentUser = userRepo.GetByID(user.us_id);
                Cloning = false;
            }
            else // Cloning
            {
                CurrentUser = user;
                userRepo.Insert(CurrentUser);
                Cloning = true;
            }
            Creating = false;
            permissionsProvider = new UserPermissionsProvider(unitOfWork, CurrentUser);
            NotifyPropertyChanged("CurrentUser");
            NotifyPropertyChanged("HeadingContext");
            NotifyPropertyChanged("IsSavable");
            NotifyPropertyChanged("UserRole");
        }

        /// <summary>
        /// Save the user to the database
        /// </summary>
        public void Save()
        {
            userRepo.Update(CurrentUser);

            if (userValidator.IsValid(unitOfWork,CurrentUser,out List<Violation> violations))
            {
                unitOfWork.Commit();

                if (navCoordinator.WaitingToNavigate)
                {
                    navCoordinator.NavigateToQueued();
                }
                else
                {
                    Creating = false;
                    Cloning = false;
                    SaveViolations = null;
                    NotifyPropertyChanged("IsSavable");
                    NotifyPropertyChanged("CurrentUser");
                    NotifyPropertyChanged("HeadingContext");
                    NotifyPropertyChanged("SaveViolations");
                    NotifyPropertyChanged("FirstNameViolation");
                    NotifyPropertyChanged("LastNameViolation");
                    NotifyPropertyChanged("DisplayNameViolation");
                    NotifyPropertyChanged("EmailViolation");
                    NotifyPropertyChanged("PasswordViolation");
                    NotifyPropertyChanged("RoleUnassignedViolation");
                }
            }
            else
            {
                SaveViolations = violations;
                NotifyPropertyChanged("SaveViolations");
                NotifyPropertyChanged("FirstNameViolation");
                NotifyPropertyChanged("LastNameViolation");
                NotifyPropertyChanged("DisplayNameViolation");
                NotifyPropertyChanged("EmailViolation");
                NotifyPropertyChanged("PasswordViolation");
                NotifyPropertyChanged("RoleUnassignedViolation");
            }
        }

        /// <summary>
        /// Delete user from the database
        /// </summary>
        public void DeleteUser()
        {
            userDeleteStrategy.Delete(unitOfWork, CurrentUser);
            unitOfWork.Commit();
            navCoordinator.NavigateToProjectSettings();
        }

        /// <summary>
        /// Role changed, update the user
        /// </summary>
        /// <param name="newValue"></param>
        public void OnRoleChanged(int newValue)
        {
            CurrentUser.us_roid = newValue;
            userRepo.Update(CurrentUser);
            NotifyPropertyChanged("IsSavable");
        }

        #region Permissions
        /// <summary>
        /// Permissions was changed from the control
        /// </summary>
        private void PermissionUpdated()
        {
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }
        /// <summary>
        /// Reset all permissions to role's defaults
        /// </summary>
        private void ResetAllPermissions()
        {
            permissionsProvider.ResetAllPermissions();
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }

        /// <summary>
        /// Deactivate all permissions and apply overrides where necessary
        /// </summary>
        public void DeactivateAllPermissions()
        {
            permissionsProvider.DeactivateAllPermissions();
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }

        /// <summary>
        /// Activate all permissions, applying overrides where necesary
        /// </summary>
        public void ActivateAllPermissions()
        {
            permissionsProvider.ActivateAllPermissions();
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }
      
        #endregion
    }
}
