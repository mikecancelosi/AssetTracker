using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quipu.Core.DomainModel
{
    public class Discussion
    {
        public Discussion()
        {
            Children = new HashSet<Discussion>();
        }

        [Key]
        public int ID { get; set; }
        public string Message_Contents { get; set; }
        public int Record_ID { get; set; }
        public int? Discussion_Parent_ID { get; set; }
        public int? User_ID { get; set; }
        public DateTime Date_Posted { get; set; }

        public virtual ICollection<Discussion> Children { get; set; }
        public virtual Discussion Parent { get; set; }
        public virtual User User { get; set; }
    }
}
