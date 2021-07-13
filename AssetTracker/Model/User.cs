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

        public override void Delete(TrackerContext context)
        {
            foreach(Asset asset in AssetsAssigned.ToList())
            {
                context.Entry(asset).Property(x => x.as_usid).CurrentValue = null;
            }

            foreach(Discussion post in Discussions.ToList())
            {
                context.Entry(post).Property(x => x.di_usid).CurrentValue = null;
            }


            base.Delete(context);
        }

        public override DatabaseBackedObject Clone()
        {
            return new User()
            {
                us_displayname = us_displayname,
                us_email = us_email,
                us_fname = us_fname,
                us_lname = us_lname,
                us_roid = us_roid
            };
        }
    }
}
