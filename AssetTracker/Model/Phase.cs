using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public partial class Phase : DatabaseBackedObject
    {
        public override int ID => ph_id;
        public override string Name => ph_name;
        public override bool IsValid(out List<Violation> violations)
        {
            violations = new List<Violation>();
            base.IsValid(out violations);
            return violations.Count() == 0;
        }

        public override void Delete(TrackerContext context)
        {
            foreach(var asset in AssetsInPhase.ToList())
            {
                context.Entry(asset).Property(x=>x.as_phid).CurrentValue = null;
            }

            base.Delete(context);
        }
    }
}
