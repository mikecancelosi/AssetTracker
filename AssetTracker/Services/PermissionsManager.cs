using DataAccessLayer;
using DomainModel;
using System.Linq;

namespace AssetTracker.Services
{
    public class PermissionsManager
    {
        public static bool HasPermission(User user, Permission permissionToCheck, GenericUnitOfWork uow)
        {
            GenericRepository<User> userRepo = uow.GetRepository<User>();
            GenericRepository<SecPermission> permissionRepo = uow.GetRepository<SecPermission>();
            GenericRepository<SecPermission2> userOverrideRepo = uow.GetRepository<SecPermission2>();
            GenericRepository<SecPermission3> roleOverrideRepo = uow.GetRepository<SecPermission3>();

            var permission = permissionRepo.GetByID((int)permissionToCheck);
            bool defaultValue = permission.pr_default;
            var roleID = user.SecRole?.ID ?? 0;

            var userOverride = (from o in userOverrideRepo.Get()
                                where o.p2_usid == user.ID
                                select o).FirstOrDefault();
            if(userOverride != null)
            {
                return userOverride.p2_allow;
            }

            var roleOverride = (from o in roleOverrideRepo.Get()
                                where o.p3_roid == roleID
                                select o).First();

            if(roleOverride != null)
            {
                return roleOverride.p3_allow;
            }

            return defaultValue;
        }
    }
}
