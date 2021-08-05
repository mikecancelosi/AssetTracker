using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.ViewModels.Interfaces;
using DataAccessLayer;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using static DomainModel.SecPermission;

namespace AssetTracker.ViewModels
{
    public class UserEditViewModel : ViewModel, ISavable
    {
        public User CurrentUser { get; private set; }

        private ObservableCollection<SecPermission.PermissionGroup> permissionGrps;
        public ObservableCollection<SecPermission.PermissionGroup> PermissionGroups
        {
            get
            {
                if (permissionGrps == null)
                {
                    List<SecPermission4> permissionGroups = (from grp in roleGroupsRepo.Get()
                                                             select grp).ToList();

                    var Grps = new List<PermissionGroup>();
                    foreach (SecPermission4 grp in permissionGroups)
                    {
                        ObservableCollection<PermissionItem> items = new ObservableCollection<PermissionItem>();
                        foreach (var permission in grp.SecPermissions)
                        {
                            bool allowed = permission.pr_default;
                            SecPermission2 permissionOverride = (from p in userOverrideRepo.Get()
                                                                 where p.p2_prid == permission.pr_id &&
                                                                 p.p2_usid == CurrentUser.ID
                                                                 orderby p.p2_id descending
                                                                 select p)?.FirstOrDefault() ?? null;

                            allowed = permissionOverride?.p2_allow ?? allowed;
                            PermissionItem item = new PermissionItem()
                            {
                                Permission = permission,
                                Allowed = allowed,
                            };
                            item.AllowedChanged += (prid, allow) => OnPermissionChanged(prid, allow);
                            items.Add(item);
                        }
                        Grps.Add(new PermissionGroup()
                        {
                            Name = grp.p4_name,
                            Height = 100 * (new Random().NextDouble()),
                            Permissions = items
                        });
                    }
                    permissionGrps = new ObservableCollection<PermissionGroup>();
                    Grps.ForEach(x => permissionGrps.Add(x));
                }

                return permissionGrps;
            }
        }

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

        public ICommand SaveCommand { get; set; }
        public ICommand RefuseSave { get; set; }
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
        public INavigationCoordinator navCoordinator { get; set; }

        public ICommand DeleteConfirmed { get; set; }

        private GenericRepository<User> userRepo;
        private GenericRepository<SecRole> roleRepo;
        private GenericRepository<SecPermission2> userOverrideRepo;
        private GenericRepository<SecPermission4> roleGroupsRepo;

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



        public UserEditViewModel(INavigationCoordinator coord, GenericUnitOfWork uow)
        {
            navCoordinator = coord;
            navCoordinator.UserNavigationAttempt += (s) => PromptSave = true;

            unitOfWork = uow;
            userRepo = unitOfWork.GetRepository<User>();
            roleRepo = unitOfWork.GetRepository<SecRole>();
            userOverrideRepo = unitOfWork.GetRepository<SecPermission2>();
            roleGroupsRepo = unitOfWork.GetRepository<SecPermission4>();

            CurrentUser = new User();
            DeleteConfirmed = new RelayCommand((s) => DeleteUser(), (s) => true);
            SaveCommand = new RelayCommand((s) => Save(), (s) => true);
            RefuseSave = new RelayCommand((s) => navCoordinator.NavigateToQueued(), (s) => true);

            NotifyPropertyChanged("UserRole");
            NotifyPropertyChanged("Roles");

            //TODO: Implement breadcrumbs
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
                    NotifyPropertyChanged("IsSavable");
                    NotifyPropertyChanged("CurrentUser");
                    NotifyPropertyChanged("HeadingContext");
                }
            }
            else
            {
                SaveViolations = violations;
                NotifyPropertyChanged("SaveViolations");
                throw new NotImplementedException();
            }
        }

        public void DeleteUser()
        {
            userRepo.Delete(CurrentUser);
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
        public void OnPermissionChanged(int prid, bool newValue)
        {
            SecPermission2 overridePer = GetUserOverride(prid);
            overridePer.p2_allow = newValue;
            NotifyPropertyChanged("IsSavable");
        }

        public void DeactivateAllPermissions()
        {
            for (int i = 0; i < PermissionGroups.Count; i++)
            {
                for (int j = 0; j < PermissionGroups[i].Permissions.Count; j++)
                {
                    SecPermission.PermissionItem item = PermissionGroups[i].Permissions[j];
                    item.Allowed = false;
                    PermissionGroups[i].Permissions[j] = item;

                    SecPermission2 overridePermission = GetUserOverride(item.Permission.ID);
                    overridePermission.p2_allow = false;
                }
            }
        }

        public void ActivateAllPermissions()
        {
            for (int i = 0; i < PermissionGroups.Count; i++)
            {
                for (int j = 0; j < PermissionGroups[i].Permissions.Count; j++)
                {
                    SecPermission.PermissionItem item = PermissionGroups[i].Permissions[j];
                    item.Allowed = true;
                    PermissionGroups[i].Permissions[j] = item;

                    SecPermission2 overridePermission = GetUserOverride(item.Permission.ID);
                    overridePermission.p2_allow = true;
                }
            }
        }

        public SecPermission2 GetUserOverride(int prid)
        {
            SecPermission2 overridePer = (from p in userOverrideRepo.Get()
                                          where p.p2_prid == prid
                                          && p.p2_usid == CurrentUser.us_id
                                          select p).FirstOrDefault();
            if (overridePer == null)
            {
                overridePer = new SecPermission2()
                {
                    p2_prid = prid,
                    p2_usid = CurrentUser.ID,
                    p2_allow = true
                };
                userOverrideRepo.Insert(overridePer);
            }

            return overridePer;
        }
        #endregion
    }
}
