using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainModel.SecPermission;

namespace AssetTracker.Services
{
    public interface IPermissionsProvider
    {
        List<PermissionGroup> PermissionGroups { get; }

        void ChangePermission(int permissionId, bool newValue);

        void ResetAllPermissions();

        void ActivateAllPermissions();

        void DeactivateAllPermissions();
    }
}
