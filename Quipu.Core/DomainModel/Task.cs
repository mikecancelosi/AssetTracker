using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quipu.Core.DomainModel
{
    public class Task
    {
        public Task()
        {
            Discussions = new HashSet<Discussion>();
            Alerts = new HashSet<Alert>();
        }

        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="A task needs to have a title")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Assigned_User_ID { get; set; }

        public virtual User AssignedToUser { get; set; }
        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
    }
}
