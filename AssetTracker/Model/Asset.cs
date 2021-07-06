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
    }
}
