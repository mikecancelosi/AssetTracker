using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.ViewModels.Interfaces;
using AssetTracker.ViewModels.Services;
using DataAccessLayer;
using DataAccessLayer.Strategies;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using static DomainModel.SecPermission;

namespace AssetTracker.ViewModels
{   
    public class RoleEditViewModel : ViewModel, ISavable
    {
        public SecRole Role { get; private set; }

        public string RoleName
        {
            get => Role.ro_name;
            set
            {
                Role.ro_name = value;
                NotifyPropertyChanged("IsSavable");
            }
        }

        public List<PermissionGroup> PermissionGroups => permissionProvider.PermissionGroups;

        public bool Creating { get; private set; }
        public bool Cloning { get; private set; }
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

        public bool IsSavable => unitOfWork.HasChanges;
        public ICommand DeleteConfirmed => new RelayCommand((s) => DeleteRole(), (s) => true);
        public ICommand SaveCommand => new RelayCommand((s) => Save(), (s) => true);
        public ICommand RefuseSave => new RelayCommand((s) => navCoordinator.NavigateToQueued(), (s) => true);
        public List<Violation> SaveViolations { get; set; }
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
        #region Commands
        public ICommand ResetAllPermissionsCommand => new RelayCommand((s) => ResetAllPermissions(), (s) => true);
        public ICommand ActivateAllPermissionsCommand => new RelayCommand((s) => ActivateAllPermissions(), (s) => true);
        public ICommand DeactivateAllPermissionsCommand => new RelayCommand((s) => DeactivateAllPermissions(), (s) => true);
        public ICommand PermissionChanged => new RelayCommand((s) => PermissionUpdated(), (s) => true);
        #endregion


        #region Repositories
        private GenericRepository<SecRole> roleRepo;
        #endregion

        #region ErrorHandling
        public Violation RoleNameViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "ro_name") ?? null;
        #endregion

        public INavigationCoordinator navCoordinator { get; set; }
        private IDeleteStrategy<SecRole> roleDeleteStrategy;

        private RolePermissionsProvider permissionProvider;
        private IModelValidator<SecRole> roleValidator;

        public RoleEditViewModel(INavigationCoordinator coord, GenericUnitOfWork uow, IDeleteStrategy<SecRole> roleDeleteStrat, IModelValidator<SecRole> roleValidatorService)
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

        public void SetRole(SecRole role)
        {
            if (role.ro_id > 0)
            {
                Role = roleRepo.GetByID(role.ro_id);
            }
            else
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

        public void DeleteRole()
        {
            roleDeleteStrategy.Delete(unitOfWork, Role);
            unitOfWork.Commit();
            navCoordinator.NavigateToProjectSettings();
        }

        public void OnRoleNameChanged(string newValue)
        {
            Role.ro_name = newValue;
            roleRepo.Update(Role);
            NotifyPropertyChanged("IsSavable");
        }

        private void PermissionUpdated()
        {
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }

        private void ResetAllPermissions()
        {
            permissionProvider.ResetAllPermissions();
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }

        private void ActivateAllPermissions()
        {
            permissionProvider.ActivateAllPermissions();
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }

        private void DeactivateAllPermissions()
        {
            permissionProvider.DeactivateAllPermissions();
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }
    }
}