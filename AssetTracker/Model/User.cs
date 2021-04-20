using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public partial class User : DatabaseBackedObject
    {

        public override int ID
        {
            get
            {
                return us_id;
            }
            set
            {
                us_id = value;
            }
        }
        
        public override string Name
        {
            get
            {
                return us_displayname;
            }
            set
            {
                us_displayname = value;
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
