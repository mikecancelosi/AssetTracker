using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public partial class Change : DatabaseBackedObject
    {
        public override bool IsValid(out List<Violation> violations)
        {
            violations = new List<Violation>();
            base.IsValid(out violations);
            return violations.Count() == 0;
        }
    }
}
