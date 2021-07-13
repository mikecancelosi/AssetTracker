using AssetTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AssetTracker.Model.SecPermission;

namespace AssetTracker.ViewModel
{
    public class RoleEditViewModel : ViewModel
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
                            items.Add(new PermissionItem()
                            {
                                Permission = permission,
                                Allowed = allowed,
                            });
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
        public bool Savable { get; set; }

        public RoleEditViewModel(SecRole role)
        {
            Role = role;
        }

        public RoleEditViewModel()
        {
            Role = context.SecRoles.Find(1);
        }

        public bool OnSave(out List<Violation> violations)
        {
            // Saving Role also saves role permissions changes         
            if (Role.Save(context, out violations))
            {               
                NotifyPropertyChanged("Savable");
                return true;
            }

            return false;
        }

        public void OnPermissionChange(bool newValue, int permissionId)
        {
            SecPermission permission = context.SecPermissions.Find(permissionId);
            SecPermission3 overridePermission = (from p in context.SecPermission3
                                                 where p.p3_prid == permissionId
                                                 && p.p3_roid == Role.ID
                                                 select p).FirstOrDefault();
            if(overridePermission == null)
            {
                overridePermission = context.SecPermission3.Create();                
                overridePermission.p3_prid = permission.pr_id;
                overridePermission.p3_roid = Role.ID;
            }

            overridePermission.p3_allow = newValue;
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
            }

            return overridePer;
        }
    }
}
