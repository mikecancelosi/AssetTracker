using DataAccessLayer;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainModel.SecPermission;

namespace AssetTracker.Services
{
    public class RolePermissionsProvider : IPermissionsProvider
    {
        public List<PermissionGroup> PermissionGroups
        {
            get
            {
                var Grps = new List<PermissionGroup>();
                foreach (SecPermission4 grp in PermissionGrps.ToList())
                {
                    List<PermissionItem> items = new List<PermissionItem>();
                    foreach (var permission in grp.SecPermissions)
                    {
                        bool allowed = permission.pr_default;
                        SecPermission3 permissionOverride = roleOverrides.FirstOrDefault(x => x.p3_prid == permission.pr_id);

                        allowed = permissionOverride?.p3_allow ?? allowed;
                        PermissionItem item = new PermissionItem()
                        {
                            Permission = permission,
                            Allowed = allowed,
                        };
                        item.AllowedChanged += (prid, allow) => ChangePermission(prid, allow);
                        items.Add(item); ;
                    }
                    Grps.Add(new PermissionGroup()
                    {
                        Name = grp.p4_name,
                        Permissions = items
                    });
                }
                return Grps;
            }
        }

        private ObservableCollection<SecPermission4> PermissionGrps
        {
            get
            {
                var grpList = (from grp in permissionHeaderRepo.Get()
                               select grp);
                return new ObservableCollection<SecPermission4>(grpList);
            }
        }

        private List<SecPermission3> roleOverrides;        

        private GenericUnitOfWork unitOfWork;
        private SecRole Role;
        private GenericRepository<SecPermission3> roleOverrideRepo;
        private GenericRepository<SecPermission4> permissionHeaderRepo;

        public RolePermissionsProvider(GenericUnitOfWork uow, SecRole role)
        {
            unitOfWork = uow;
            Role = role;

            roleOverrideRepo = unitOfWork.GetRepository<SecPermission3>();
            permissionHeaderRepo = unitOfWork.GetRepository<SecPermission4>();

            roleOverrides = (from o in roleOverrideRepo.Get()
                             where o.p3_roid == Role.ID
                             select o).ToList();
        }



        public void ChangePermission(int permissionId, bool newValue)
        {
            SecPermission3 overridePermission = GetRoleOverride(permissionId);
            overridePermission.p3_allow = newValue;
        }

        public void ResetAllPermissions()
        {
            List<SecPermission3> overrideList = (from o in roleOverrideRepo.Get()
                                                 where o.p3_roid == Role.ro_id
                                                 select o).ToList();
            foreach (SecPermission3 permission in overrideList)
            {
                roleOverrideRepo.Delete(permission);
            }

            roleOverrides.Clear();
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
                    roleOverrideRepo.Update(overridePermission);
                }
            }
        }

        private SecPermission3 GetRoleOverride(int prid)
        {
            SecPermission3 overridePer = roleOverrides.FirstOrDefault(x => x.p3_prid == prid);

            if (overridePer == null)
            {
                overridePer = new SecPermission3()
                {
                    p3_prid = prid,
                    p3_roid = Role.ID,
                    p3_allow = true,
                };
                roleOverrides.Add(overridePer);
                roleOverrideRepo.Insert(overridePer);
            }

            return overridePer;
        }
    }
}
