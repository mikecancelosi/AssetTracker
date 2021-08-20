using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quipu.Core.DomainModel
{
    public class PermissionGroup
    {
        public PermissionGroup()
        {
            SecPermissions = new HashSet<Permission>();
        }

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Permission> SecPermissions { get; set; }
    }
}
