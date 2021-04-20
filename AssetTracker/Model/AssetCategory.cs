using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public partial class AssetCategory : DatabaseBackedObject
    {

        public override int ID
        {
            get
            {
                return ca_id;
            }
            set
            {
                ca_id = value;
            }
        }
        public override string Name
        {
            get
            {
                return ca_name;
            }
            set
            {
                ca_name = value;
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
