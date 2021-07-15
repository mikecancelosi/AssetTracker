using AssetTracker.Enums;
using AssetTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            base.IsValid(out violations);
            return violations.Count() == 0;
        }

        public override List<Change> GetChanges(DatabaseBackedObject beforeObject)
        {
            List<Change> output = base.GetChanges(beforeObject);
            Asset beforeAsset = ((Asset)beforeObject);
            if (this.as_usid != beforeAsset.as_usid)
            {
                output.Add(new Change()
                {
                     ch_description = ChangeType.UserAssigned,
                     ch_datetime = DateTime.Now,
                     ch_oldvalue = beforeAsset.as_usid.ToString(),
                     ch_newvalue = as_usid.ToString(),
                     ch_field =  "as_usid",
                     ch_recid = ID,
                     ch_usid = MainViewModel.Instance.CurrentUser.ID

                });
            }
            return output;
        }

        public List<Alert> GetAlerts(Asset beforeObject)
        {
            List<Alert> output = new List<Alert>();
            if(this.as_usid != beforeObject.as_usid)
            {
                output.Add(new Alert()
                {
                     ar_asid = as_id,
                     ar_date = DateTime.Now,
                     ar_projectwide = false,
                     ar_viewed = false,
                     ar_usid = as_usid,
                     ar_priority = false,
                     ar_content = Phase.ph_name + " | " + as_displayname,
                     ar_recid = MainViewModel.Instance.CurrentUser.ID,
                     ar_type = "Assign"
                });
            }
            return output;
        }

        public override void Delete(TrackerContext context)
        {
            if(this.AssetLink != null)
            {
                AssetLink.Delete(context);
            }
            if(this.as_usid > 0)
            {
                context.Entry(this).Property(x => x.as_usid).CurrentValue = 0;
            }

            base.Delete(context);
        }

        public override DatabaseBackedObject Clone()
        {
            //TODO : Copy with children
            Asset copy = new Asset()
            {
                as_caid = this.as_caid,
                as_description = this.as_description,
                as_displayname = this.as_displayname,
                as_parentid = this.as_parentid,
                as_usid = this.as_usid,
                as_phid = this.as_phid
            };
            return copy;
        }
    }
}
