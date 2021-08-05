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
    public class RoleEditViewModel : ViewModel, ISavable
    {
        public SecRole Role { get; set; }

        public string RoleName
        {
            get => Role.ro_name;
            set
            {
                Role.ro_name = value;
                NotifyPropertyChanged("IsSavable");
            }
        }

        private ObservableCollection<PermissionGroup> permissionGrps;
        public ObservableCollection<PermissionGroup> PermissionGroups
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
                            SecPermission3 permissionOverride = (from p in roleOverrideRepo.Get()
                                                                 where p.p3_prid == permission.pr_id &&
                                                                 p.p3_roid == Role.ro_id
                                                                 orderby p.p3_id descending
                                                                 select p)?.FirstOrDefault() ?? null;

                            allowed = permissionOverride?.p3_allow ?? allowed;
                            PermissionItem item = new PermissionItem()
                            {
                                Permission = permission,
                                Allowed = allowed,
                            };
                            item.AllowedChanged += (prid, allow) => OnPermissionChanged(prid, allow);
                            items.Add(item); ;
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
        public bool Cloning { get; set; }
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
        public ICommand DeleteConfirmed { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand RefuseSave { get; set; }
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

        public ICommand NavigateToProjectSettingsCommand => new RelayCommand((s) => navCoordinator.NavigateToProjectSettings(),
                                                                             (s) => true);

        private GenericRepository<SecPermission4> roleGroupsRepo;
        private GenericRepository<SecPermission3> roleOverrideRepo;
        private GenericRepository<SecRole> roleRepo;


        public INavigationCoordinator navCoordinator { get; set; }
        private IDeleteStrategy<SecRole> roleDeleteStrategy;

        public RoleEditViewModel(INavigationCoordinator coord, GenericUnitOfWork uow, IDeleteStrategy<SecRole> roleDeleteStrat)
        {
            navCoordinator = coord;
            roleDeleteStrategy = roleDeleteStrat;
            navCoordinator.UserNavigationAttempt += (s) => PromptSave = true;

            unitOfWork = uow;
            roleGroupsRepo = unitOfWork.GetRepository<SecPermission4>();
            roleOverrideRepo = unitOfWork.GetRepository<SecPermission3>();
            roleRepo = unitOfWork.GetRepository<SecRole>();

            Role = new SecRole();
            DeleteConfirmed = new RelayCommand((s) => DeleteRole(), (s) => true);
            SaveCommand = new RelayCommand((s) => Save(), (s) => true);
            RefuseSave = new RelayCommand((s) => navCoordinator.NavigateToQueued(), (s) => true);
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
            Creating = false;
            NotifyPropertyChanged("Role");
            NotifyPropertyChanged("HeadingContext");
        }

        public void Save()
        {
            roleRepo.Update(Role);

            if (Role.IsValid(out List<Violation> violations))
            {
                unitOfWork.Commit();
                NotifyPropertyChanged("IsSavable");
                NotifyPropertyChanged("Role");
                Creating = false;
                Cloning = false;
                NotifyPropertyChanged("HeadingContext");
            }
            else
            {
                SaveViolations = violations;
                NotifyPropertyChanged("SaveViolations");
                throw new NotImplementedException();
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

        public void OnPermissionChanged(int permissionId, bool newValue)
        {
            SecPermission3 overridePermission = GetRoleOverride(permissionId);
            overridePermission.p3_allow = newValue;
            NotifyPropertyChanged("IsSavable");
        }

        public void DeactivateAllPermissions()
        {
            for (int i = 0; i < PermissionGroups.Count; i++)
            {
                for (int j = 0; j < PermissionGroups[i].Permissions.Count; j++)
                {
                    PermissionItem item = PermissionGroups[i].Permissions[j];
                    item.Allowed = false;
                    PermissionGroups[i].Permissions[j] = item;

                    SecPermission3 overridePermission = GetRoleOverride(item.Permission.ID);
                    overridePermission.p3_allow = false;
                }
            }
        }

        public void ActivateAllPermissions()
        {
            for (int i = 0; i < PermissionGroups.Count; i++)
            {
                for (int j = 0; j < PermissionGroups[i].Permissions.Count; j++)
                {
                    PermissionItem item = PermissionGroups[i].Permissions[j];
                    item.Allowed = true;
                    PermissionGroups[i].Permissions[j] = item;

                    SecPermission3 overridePermission = GetRoleOverride(item.Permission.ID);
                    overridePermission.p3_allow = true;
                }
            }
        }

        public SecPermission3 GetRoleOverride(int prid)
        {
            SecPermission3 overridePer = (from p in roleOverrideRepo.Get()
                                          where p.p3_prid == prid
                                          && p.p3_roid == Role.ID
                                          select p).FirstOrDefault();
            if (overridePer == null)
            {
                overridePer = new SecPermission3();
                overridePer.p3_prid = prid;
                overridePer.p3_roid = Role.ID;
                overridePer.p3_allow = true;
                roleOverrideRepo.Insert(overridePer);
            }

            return overridePer;
        }
    }
}
