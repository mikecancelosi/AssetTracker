using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Quipu.Core.DomainModel
{
    public class Alert
    {
        [Key]
        public int ID { get; set; }
        public string Type { get; set; }
        public int? User_ID { get; set; }
        public bool? Prioritized { get; set; }
        public int? RecordID { get; set; }
        public DateTime Date { get; set; }
        public bool Viewed { get; set; }
        public string Content { get; set; }
        public string Header { get; set; }

        public virtual User AlertedUser { get; set; }
    }
}
