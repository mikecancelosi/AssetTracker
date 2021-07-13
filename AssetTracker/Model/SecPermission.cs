using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public partial class SecPermission : DatabaseBackedObject
    {
        public override int ID
        {
            get
            {
                return  pr_id;
            }
            set
            {
                pr_id = value;
            }
        }
        public override string Name
        {
            get
            {
                return pr_name;
            }
            set
            {
                pr_name = value;
            }
        }

        // Structs used for displaying permissions in roleEdit and user edit
        public struct PermissionGroup
        {
            public string Name { get; set; }
            public double Height { get; set; }

            public ObservableCollection<PermissionItem> Permissions { get; set; }
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
                    AllowedChanged?.Invoke(Permission.ID,Allowed);

                }
            }

            public delegate void ValueChange(int prid, bool allowed);
            public event ValueChange AllowedChanged; 
        }

    }
}
