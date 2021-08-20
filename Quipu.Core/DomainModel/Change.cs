using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quipu.Core.DomainModel
{
    public class Change
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string AssociatedField { get; set; }
        public int RecordID { get; set; }
        public int UserID { get; set; }

        public virtual User UserWhoMadeChange { get; set; }
    }
}
