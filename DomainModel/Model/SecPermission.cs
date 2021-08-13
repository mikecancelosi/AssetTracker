using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DomainModel
{
    /// <summary>
    /// Sec Permission (1) are the actual permissions users/ roles 
    /// can have access to.
    /// </summary>
    public partial class SecPermission : DatabaseBackedObject
    {
        public override int ID
        {
            get => pr_id;
            set => pr_id = value;
        }

        public override string Name
        {
            get => pr_name;
            set => pr_name = value;
        }

        // Structs used for displaying permissions in roleEdit and user edit
        public struct PermissionGroup
        {
            public string Name { get; set; }

            public List<PermissionItem> Permissions { get; set; }
        }

        public struct PermissionItem
        {
            public SecPermission Permission { get; set; }

            private bool allowed;
            public bool Allowed 
            {
                get
                {
                    return allowed;
                }
                set
                {
                    allowed = value;
                    if((AllowedChanged?.GetInvocationList().Length ?? 0) > 0)
                    {
                        IsOverride = true;
                    }
                    AllowedChanged?.Invoke(Permission.ID,Allowed);
                }
            }
            public bool IsOverride { get; set; }

            public delegate void ValueChange(int prid, bool allowed);
            public event ValueChange AllowedChanged; 
        }

    }
}
