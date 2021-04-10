using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public partial class SecRole : DatabaseBackedObject
    {

        public override int ID => ro_id;
        public override string Name => ro_name;
        public override bool IsValid(out List<Violation> violations)
        {
            violations = new List<Violation>();
            base.IsValid(out violations);
            return violations.Count() == 0;
        }
    }
}
