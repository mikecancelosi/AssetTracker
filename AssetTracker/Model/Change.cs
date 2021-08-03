using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    /// <summary>
    /// A change is logged after asset saves in order for users to
    /// be able to view a changelog ( left hand side of asset detail )
    /// </summary>
    public partial class Change : DatabaseBackedObject
    {
        public override int ID
        {
            get => ch_id;            
            set => ch_id = value;
        }

        public override string Name         
        {
            get => ch_field;
            set => ch_field = value;
        }

        public override bool IsValid(out List<Violation> violations)
        {
            violations = new List<Violation>();
            base.IsValid(out violations);
            return violations.Count() == 0;
        }
    }
}
