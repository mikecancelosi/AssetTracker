using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quipu.Core.DomainModel
{
    public class User
    {
        public User()
        {
            this.TasksAssigned = new HashSet<Task>();
            this.UserPermissionOverrides = new HashSet<UserPermissionOverride>();
            this.Discussions = new HashSet<Discussion>();
            this.Alerts = new HashSet<Alert>();
        }

        [Key]
        public int ID { get; set; }
        public string Display_Name { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Task> TasksAssigned { get; set; }
        public virtual ICollection<UserPermissionOverride> UserPermissionOverrides { get; set; }
        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
    }
}
