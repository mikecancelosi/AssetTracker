using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quipu.Core.DomainModel
{
    public class Permission
    {
        public Permission()
        {
            this.UserPermissionOverrides = new HashSet<UserPermissionOverride>();
        }

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Header_ID { get; set; }
        public bool Default { get; set; }

        public virtual ICollection<UserPermissionOverride> UserPermissionOverrides { get; set; }
        public virtual PermissionGroup PermissionHeader { get; set; }
    }
}
