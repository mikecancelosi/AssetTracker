﻿using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static AssetTracker.Model.SecPermission;

namespace AssetTracker.ViewModels
{
    public class RoleEditViewModel : ViewModel, ISavable
    {
        public SecRole Role { get; set; }

        private ObservableCollection<PermissionGroup> permissionGrps;
        public ObservableCollection<PermissionGroup> PermissionGroups
        {
            get
            {
                if (permissionGrps == null)
                {
                    List<SecPermission4> permissionGroups = (from grp in context.SecPermission4
                                                             select grp).ToList();

                    var Grps = new List<PermissionGroup>();
                    foreach (SecPermission4 grp in permissionGroups)
                    {
                        ObservableCollection<PermissionItem> items = new ObservableCollection<PermissionItem>();
                        foreach (var permission in grp.SecPermissions)
                        {
                            bool allowed = permission.pr_default;
                            SecPermission3 permissionOverride = (from p in context.SecPermission3
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
                            Height = 100 *( new Random().NextDouble()),
                            Permissions = items
                        });
                    }
                    permissionGrps = new ObservableCollection<PermissionGroup>();
                    Grps.ForEach(x=> permissionGrps.Add(x));                    
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

        public bool IsSavable => context.ChangeTracker.HasChanges();
        public ICommand DeleteConfirmed { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand RefuseSave { get; set; }
        public List<Violation> SaveViolations { get; set; }
        public bool PromptSave { get; set; }

        public INavigationCoordinator navCoordinator { get; set; }
        public RoleEditViewModel(INavigationCoordinator coord)
        {
            navCoordinator = coord;
            DeleteConfirmed = new RelayCommand((s) => DeleteRole(), (s) => true);
            SaveCommand = new RelayCommand((s) => Save(), (s) => true);
            RefuseSave = new RelayCommand((s) => navCoordinator.NavigateToQueued(), (s) => true);

        }

        public void SetRole(SecRole role)
        {
            if (role.ro_id > 0)
            {
                Role = context.SecRoles.Find(role.ro_id);               
            }
            else
            {
                Role = context.SecRoles.Attach(role);
                Cloning = true;
            }
            Creating = false;
            NotifyPropertyChanged("Role");
            NotifyPropertyChanged("HeadingContext");
        }

        public void Save()
        {
            List<Violation> violations;     
            if (Role.Save(context, out violations))
            {
                context.SaveChanges();
                NotifyPropertyChanged("Savable");
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
            Role.Delete(context);
            navCoordinator.NavigateToProjectSettings();
        }

        public void OnRoleNameChanged(string newValue)
        {
            context.Entry(Role).Property(x => x.ro_name).CurrentValue = newValue;
            NotifyPropertyChanged("Savable");
        }

        public void OnPermissionChanged(int permissionId, bool newValue)
        {
            SecPermission3 overridePermission = GetRoleOverride(permissionId);
            overridePermission.p3_allow = newValue;
            NotifyPropertyChanged("Savable");
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
            SecPermission3 overridePer = (from p in context.SecPermission3
                                          where p.p3_prid == prid
                                          && p.p3_roid ==  Role.ID
                                          select p).FirstOrDefault();
            if (overridePer == null)
            {
                overridePer = context.SecPermission3.Create();
                overridePer.p3_prid = prid;
                overridePer.p3_roid = Role.ID;
                overridePer.p3_allow = true;
                context.SecPermission3.Add(overridePer);
            }

            return overridePer;
        }
    }
}
