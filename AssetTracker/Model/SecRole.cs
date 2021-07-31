using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Model
{
    public partial class SecRole : DatabaseBackedObject
    {

        public override int ID
        {
            get => ro_id;
            set => ro_id = value;
        }

        public override string Name
        {
            get => ro_name;
            set => ro_name = value;
        }

        public override bool IsValid(out List<Violation> violations)
        {
            violations = new List<Violation>();
            base.IsValid(out violations);
            return violations.Count() == 0;
        }

        public override void Delete(TrackerContext context)
        {
            foreach(SecPermission3 overridePer in SecPermission3.ToList())
            {
                overridePer.Delete(context);
            }
            foreach(User user in Users.ToList())
            {
                context.Entry(user).Property(x => x.us_roid).CurrentValue = context.SecRoles.FirstOrDefault().ID;
            }

            base.Delete(context);
        }
    }
}
