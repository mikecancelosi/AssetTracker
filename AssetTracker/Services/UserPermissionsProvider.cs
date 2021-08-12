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
    public class UserPermissionsProvider : IPermissionsProvider
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
                        SecPermission2 userPermissionOverride = userOverrides.FirstOrDefault(x => x.p2_prid == permission.pr_id);
                        if(userPermissionOverride != null)
                        {
                            allowed = userPermissionOverride.p2_allow;
                        }
                        else
                        {
                            SecPermission3 permissionOverride = roleOverrides.FirstOrDefault(x => x.p3_prid == permission.pr_id);
                            allowed = permissionOverride?.p3_allow ?? allowed;
                        }
                       
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

        private List<SecPermission2> userOverrides;
        private List<SecPermission3> roleOverrides;

        private GenericUnitOfWork unitOfWork;
        private User myUser;
        
        private GenericRepository<SecPermission2> userOverrideRepo;
        private GenericRepository<SecPermission3> roleOverrideRepo;
        private GenericRepository<SecPermission4> permissionHeaderRepo;

        public UserPermissionsProvider(GenericUnitOfWork uow, User user)
        {
            unitOfWork = uow;
            myUser = user;

            roleOverrideRepo = unitOfWork.GetRepository<SecPermission3>();
            permissionHeaderRepo = unitOfWork.GetRepository<SecPermission4>();
            userOverrideRepo = unitOfWork.GetRepository<SecPermission2>();

            userOverrides = (from o in userOverrideRepo.Get()
                             where o.p2_usid == user.ID
                             select o).ToList();

            roleOverrides = (from o in roleOverrideRepo.Get()
                             where o.p3_roid == user.us_roid
                             select o).ToList();

            

        }



        public void ChangePermission(int permissionId, bool newValue)
        {
            SecPermission2 overridePermission = GetUserOverride(permissionId);
            overridePermission.p2_allow = newValue;
        }

        public void ResetAllPermissions()
        {
            List<SecPermission2> overrideList = (from o in userOverrideRepo.Get()
                                                 where o.p2_usid == myUser.ID
                                                 select o).ToList();
            foreach (SecPermission2 permission in overrideList)
            {
                userOverrideRepo.Delete(permission);
            }

            userOverrides.Clear();
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
                    userOverrideRepo.Update(overridePermission);
                }
            }
        }

        private SecPermission2 GetUserOverride(int prid)
        {
            SecPermission2 overridePer = userOverrides.FirstOrDefault(x => x.p2_prid == prid);

            if (overridePer == null)
            {
                overridePer = new SecPermission2()
                {
                    p2_prid = prid,
                    p2_usid = myUser.ID,
                    p2_allow = true,
                };
                userOverrides.Add(overridePer);
                userOverrideRepo.Insert(overridePer);
            }

            return overridePer;
        }
    }
}
