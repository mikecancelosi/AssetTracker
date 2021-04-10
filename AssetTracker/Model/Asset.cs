using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public partial class Asset : DatabaseBackedObject
    {

        public override int ID => as_id;
        public override string Name => as_displayname;
       
        public override bool IsValid(out List<Violation> violations)
        {           
            violations = new List<Violation>();
            base.IsValid(out violations);
            return violations.Count() == 0;
        }
    }
}
