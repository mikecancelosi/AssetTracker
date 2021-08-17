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
    public class RoleEditViewModel : ViewModel, ISavable
    {
        /// <summary>
        /// Role to modify
        /// </summary>
        public SecRole Role { get; private set; }

        /// <summary>
        /// Role's name 
        /// </summary>
        public string RoleName
        {
            get => Role.ro_name;
            set
            {
                Role.ro_name = value;
                roleRepo.Update(Role);
                NotifyPropertyChanged("IsSavable");
            }
        }

        /// <summary>
        /// Permissions of the role
        /// </summary>
        public List<PermissionGroup> PermissionGroups => permissionProvider.PermissionGroups;

        /// <summary>
        /// Is this a new role?
        /// </summary>
        public bool Creating { get; private set; }
        /// <summary>
        /// Are we cloning a role?
        /// </summary>
        public bool Cloning { get; private set; }
        /// <summary>
        /// Display title based on whether we are creating, cloning, or modifying.
        /// </summary>
        public string HeadingContext
        {
            get
            {
                if (Creating)
                {
                    return "Create Role";
                }
                else if (Cloning)
                {
                    return "Clone Role";
                }
                else
                {
                    return "Modify Role";
                }
            }
        }

        #region Saving
        /// <summary>
        /// Does the uow have any changes that can be saved
        /// </summary>
        public bool IsSavable => unitOfWork.HasChanges;        
        /// <summary>
        /// Execute save functionality
        /// </summary>
        public ICommand SaveCommand => new RelayCommand((s) => Save(), (s) => true);
        /// <summary>
        /// User refused to save after being prompted, navigate away.
        /// </summary>
        public ICommand RefuseSave => new RelayCommand((s) => navCoordinator.NavigateToQueued(), (s) => true);
        /// <summary>
        /// Violations detected when saving was attempted
        /// </summary>
        public List<Violation> SaveViolations { get; set; }
        /// <summary>
        /// Backing field for PromptSave
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
        /// <summary>
        /// Validator to use when saving the role
        /// </summary>
        private IModelValidator<SecRole> roleValidator;
        #endregion

        #region Commands
        public ICommand ResetAllPermissionsCommand => new RelayCommand((s) => ResetAllPermissions(), (s) => true);
        public ICommand ActivateAllPermissionsCommand => new RelayCommand((s) => ActivateAllPermissions(), (s) => true);
        public ICommand DeactivateAllPermissionsCommand => new RelayCommand((s) => DeactivateAllPermissions(), (s) => true);
        public ICommand PermissionChanged => new RelayCommand((s) => PermissionUpdated(), (s) => true);
        #endregion

        #region Repositories
        private IRepository<SecRole> roleRepo;
        #endregion

        #region ErrorHandling
        public Violation RoleNameViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "ro_name") ?? null;
        #endregion

        public INavigationCoordinator navCoordinator { get; set; }

        #region Deleting
        private IDeleteStrategy<SecRole> roleDeleteStrategy;
        public ICommand DeleteConfirmed => new RelayCommand((s) => DeleteRole(), (s) => true);
        #endregion

        private RolePermissionsProvider permissionProvider;


        public RoleEditViewModel(INavigationCoordinator coord, IUnitOfWork uow, IDeleteStrategy<SecRole> roleDeleteStrat, IModelValidator<SecRole> roleValidatorService)
        {
            navCoordinator = coord;
            roleDeleteStrategy = roleDeleteStrat;
            unitOfWork = uow;

            navCoordinator.UserNavigationAttempt += (s) => PromptSave = true;
            roleRepo = unitOfWork.GetRepository<SecRole>();

            Role = new SecRole();
            permissionProvider = new RolePermissionsProvider(unitOfWork, Role);
            Creating = true;
        }

        /// <summary>
        /// Set bound role
        /// </summary>
        /// <param name="role">Role to bind to</param>
        public void SetRole(SecRole role)
        {
            if (role.ro_id > 0) // Modifying
            {
                Role = roleRepo.GetByID(role.ro_id);
                Cloning = false;
            }
            else //Cloning
            {
                Role = role;
                roleRepo.Insert(Role);
                Cloning = true;
            }
            permissionProvider = new RolePermissionsProvider(unitOfWork, Role);
            Creating = false;
            NotifyPropertyChanged("Role");
            NotifyPropertyChanged("HeadingContext");
        }

        /// <summary>
        /// Save the role to db
        /// </summary>
        public void Save()
        {
            roleRepo.Update(Role);

            if (roleValidator.IsValid(unitOfWork,Role,out List<Violation> violations))
            {
                unitOfWork.Commit();

                if (navCoordinator.WaitingToNavigate)
                {
                    navCoordinator.NavigateToQueued();
                }
                else
                {
                    NotifyPropertyChanged("IsSavable");
                    NotifyPropertyChanged("Role");
                    Creating = false;
                    Cloning = false;
                    SaveViolations = null;
                    NotifyPropertyChanged("SaveViolations");
                    NotifyPropertyChanged("RoleNameViolation");
                    NotifyPropertyChanged("HeadingContext");
                }
            }
            else
            {
                SaveViolations = violations;
                NotifyPropertyChanged("SaveViolations");
                NotifyPropertyChanged("RoleNameViolation");
            }
        }

        /// <summary>
        /// Delete the role from local and server db
        /// </summary>
        public void DeleteRole()
        {
            roleDeleteStrategy.Delete(unitOfWork, Role);
            unitOfWork.Commit();
            navCoordinator.NavigateToProjectSettings();
        }
        #region Permissions
        /// <summary>
        /// A permission was updated through the control
        /// </summary>
        private void PermissionUpdated()
        {
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }

        /// <summary>
        /// The user chose to reset all permissions back to their default values.
        /// </summary>
        private void ResetAllPermissions()
        {
            permissionProvider.ResetAllPermissions();
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }

        /// <summary>
        /// The user has chosen to activate all permissions, and apply overrides where necessary
        /// </summary>
        private void ActivateAllPermissions()
        {
            permissionProvider.ActivateAllPermissions();
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }

        /// <summary>
        /// The user has chosen to deactivate all permissions, applying overrides where necessary
        /// </summary>
        private void DeactivateAllPermissions()
        {
            permissionProvider.DeactivateAllPermissions();
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }
        #endregion
    }
}