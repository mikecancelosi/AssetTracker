using System.Collections.Generic;
using static DomainModel.SecPermission;

namespace Quipu.Services
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
