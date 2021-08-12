using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.ViewModels.Interfaces;
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
    /// <summary>
    /// TODO: Improve override visuals to show vs default/ role override
    /// TODO: Allow setting of profile picture
    /// </summary>
    public class UserEditViewModel : ViewModel, ISavable
    {
        public User CurrentUser { get; private set; }

        public bool Creating { get; set; }
        private bool Cloning { get; set; }
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

        #region Saving
        public ICommand SaveCommand => new RelayCommand((s) => Save(), (s) => true);
        public ICommand RefuseSave => new RelayCommand((s) => navCoordinator.NavigateToQueued(), (s) => true);
        public bool IsSavable => unitOfWork.HasChanges;
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
        #endregion

        public INavigationCoordinator navCoordinator { get; set; }

        #region Deleting
        private IDeleteStrategy<User> userDeleteStrategy;
        public ICommand DeleteConfirmed => new RelayCommand((s) => DeleteUser(), (s) => true);
        #endregion

        public List<PermissionGroup> PermissionGroups => permissionsProvider.PermissionGroups;
        public ICommand ResetAllPermissionsCommand => new RelayCommand((s) => ResetAllPermissions(), (s) => true);
        public ICommand ActivateAllPermissionsCommand => new RelayCommand((s) => ActivateAllPermissions(), (s) => true);
        public ICommand DeactivateAllPermissionsCommand => new RelayCommand((s) => DeactivateAllPermissions(), (s) => true);
        public ICommand PermissionChanged => new RelayCommand((s) => NotifyPropertyChanged("IsSavable"), (s) => true);
        private UserPermissionsProvider permissionsProvider;


        #region Repositories
        private GenericRepository<User> userRepo;
        private GenericRepository<SecRole> roleRepo;
        #endregion

        #region ErrorChecks
        public Violation FirstNameViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "us_fname") ?? null;
        public Violation LastNameViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "us_lname") ?? null;
        public Violation DisplayNameViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "us_displayname") ?? null;
        public Violation EmailViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "us_email") ?? null;
        public Violation PasswordViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "us_password") ?? null;
        public Violation RoleUnassignedViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "us_roid") ?? null;
        #endregion


        public List<DatabaseBackedObject> Roles
        {
            get
            {
                return (from r in roleRepo.Get()
                        select r).ToList<DatabaseBackedObject>();
            }
        }

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

        public UserEditViewModel(INavigationCoordinator coord, GenericUnitOfWork uow, IDeleteStrategy<User> userDeleteStrat)
        {
            navCoordinator = coord;
            userDeleteStrategy = userDeleteStrat;
            unitOfWork = uow;

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

        public void SetUser(User user)
        {
            if (user.us_id > 0)
            {
                CurrentUser = userRepo.GetByID(user.us_id);
                Creating = false;
            }
            else
            {
                CurrentUser = user;
                userRepo.Insert(CurrentUser);
                Creating = false;
                Cloning = true;
            }
            permissionsProvider = new UserPermissionsProvider(unitOfWork, CurrentUser);
            NotifyPropertyChanged("CurrentUser");
            NotifyPropertyChanged("HeadingContext");
            NotifyPropertyChanged("IsSavable");
            NotifyPropertyChanged("UserRole");
        }


        public void Save()
        {
            userRepo.Update(CurrentUser);

            if (CurrentUser.IsValid(out List<Violation> violations))
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

        public void DeleteUser()
        {
            userDeleteStrategy.Delete(unitOfWork, CurrentUser);
            unitOfWork.Commit();
            navCoordinator.NavigateToProjectSettings();
        }

        public void OnRoleChanged(int newValue)
        {
            CurrentUser.us_roid = newValue;
            userRepo.Update(CurrentUser);
            NotifyPropertyChanged("IsSavable");
        }

        #region Permissions

        private void ResetAllPermissions()
        {
            permissionsProvider.ResetAllPermissions();
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }

        public void DeactivateAllPermissions()
        {
            permissionsProvider.DeactivateAllPermissions();
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }

        public void ActivateAllPermissions()
        {
            permissionsProvider.ActivateAllPermissions();
            NotifyPropertyChanged("PermissionGroups");
            NotifyPropertyChanged("IsSavable");
        }
      
        #endregion
    }
}
