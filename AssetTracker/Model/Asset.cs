using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public partial class Asset : DatabaseBackedObject
    {

        public override int ID 
        {
            get
            {
                return as_id;
            }
            set
            {
                as_id = value;
            }
        }
        public override string Name
        {
            get
            {
                return as_displayname;
            }
            set
            {
                as_displayname = value;
            }
        }
        
       
        public override bool IsValid(out List<Violation> violations)
        {           
            violations = new List<Violation>();
            base.IsValid(out violations);
            return violations.Count() == 0;
        }
    }
}
