using AssetTracker.Model;
using AssetTracker.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static AssetTracker.Model.SecPermission;

namespace AssetTracker.ViewModels
{
    public class UserEditViewModel : ViewModel
    {
        public User CurrentUser { get; set; }
       
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
                            SecPermission2 permissionOverride = (from p in context.SecPermission2
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
                            item.AllowedChanged += (prid,allow) => OnPermissionChanged(prid, allow);
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

        public bool Savable
        {
            get
            {
                return context.ChangeTracker.HasChanges();
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
                else if(Cloning)
                {
                    return "Clone User";
                } 
                else
                {
                    return "Modify User";
                }
            }
        }

        public UserEditViewModel(User user)
        {
            if (user.us_id > 0)
            {
                CurrentUser = context.Users.Find(user.us_id);
                Creating = false;
            }
            else
            {
                CurrentUser = context.Users.Attach(user);
                Creating = false;
                Cloning = true;
            }
            NotifyPropertyChanged("CurrentUser");
            NotifyPropertyChanged("HeadingContext");
        }

        /// <summary>
        /// We are creating a user
        /// </summary>
        public UserEditViewModel()
        {
            CurrentUser = context.Users.Create();
            Creating = true;
            context.Users.Add(CurrentUser);
        }

        public bool OnSave(out List<Violation> violations)
        {
            if(CurrentUser.Save(context, out violations))
            {
                context.SaveChanges();
                NotifyPropertyChanged("Savable");
                NotifyPropertyChanged("CurrentUser");
                Creating = false;
                Cloning = false;
                NotifyPropertyChanged("HeadingContext");
                return true;
            }

            return false;
        }

        public void DeleteUser()
        {
            CurrentUser.Delete(context);
        }

        public void OnFirstNameChanged(string newName)
        {
            context.Entry(CurrentUser).Property(x => x.us_fname).CurrentValue = newName;
            NotifyPropertyChanged("Savable");
        }

        public void OnLastNameChanged(string newName)
        {
            context.Entry(CurrentUser).Property(x => x.us_lname).CurrentValue = newName;
            NotifyPropertyChanged("Savable");
        }

        public void OnDisplayNameChanged(string newName)
        {
            context.Entry(CurrentUser).Property(x => x.us_displayname).CurrentValue = newName;
            NotifyPropertyChanged("Savable");
        }

        public void OnEmailChanged(string newValue)
        {
            context.Entry(CurrentUser).Property(x => x.us_email).CurrentValue = newValue;
            NotifyPropertyChanged("Savable");
        }

        public void OnPasswordChanged(string newValue)
        {
            context.Entry(CurrentUser).Property(x => x.us_password).CurrentValue = newValue;
            NotifyPropertyChanged("Savable");
        }

        public void OnRoleChanged(int newValue)
        {
            context.Entry(CurrentUser).Property(x => x.us_roid).CurrentValue = newValue;
            NotifyPropertyChanged("Savable");
        }

        public void OnPermissionChanged(int prid, bool newValue)
        {
            SecPermission2 overridePer = GetUserOverride(prid);
            overridePer.p2_allow = newValue;
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
                    PermissionItem item = PermissionGroups[i].Permissions[j];
                    item.Allowed = true;
                    PermissionGroups[i].Permissions[j] = item;

                    SecPermission2 overridePermission = GetUserOverride(item.Permission.ID);
                    overridePermission.p2_allow = true;
                }
            }
        }

        public SecPermission2 GetUserOverride(int prid)
        {
            SecPermission2 overridePer = (from p in context.SecPermission2
                                          where p.p2_prid == prid
                                          && p.p2_usid == CurrentUser.us_id
                                          select p).FirstOrDefault();
            if (overridePer == null)
            {
                overridePer = context.SecPermission2.Create();
                overridePer.p2_prid = prid;
                overridePer.p2_usid = CurrentUser.ID;
                overridePer.p2_allow = true;
                context.SecPermission2.Add(overridePer);                
            }

            return overridePer;
        }
    }
}
