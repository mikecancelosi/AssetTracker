using DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel
{
    /// <summary>
    /// Asset is the main object in AssetTracker. It represents any object that 
    /// needs to be worked on.
    /// </summary>
    public partial class Asset : DatabaseBackedObject
    {
        public override int ID
        {
            get => as_id;
            set => as_id = value;
        }

        public override string Name
        {
            get => as_displayname;
            set => as_displayname = value;
        }

        public override bool IsValid(out List<Violation> violations)
        {
            base.IsValid(out violations);

            if(as_description == "" || as_description == null)
            {
                violations.Add(new Violation("You need to add a description!", "as_description"));
            }

            if (as_displayname == "" || as_description == null)
            {
                violations.Add(new Violation("You need to set the name!", "as_displayname"));
            }

            return violations.Count() == 0;
        }

        public List<Change> GetChanges(DatabaseBackedObject beforeObject,User currentUser)
        {
            List<Change> output = new List<Change>();
            Asset beforeAsset = ((Asset)beforeObject) ?? new Asset();
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
                     ch_usid = currentUser.ID

                });
            }
            if((this?.as_phid ?? 0) != beforeAsset.as_phid)
            {
                output.Add(new Change()
                {
                    ch_description = ChangeType.ChangedPhase,
                    ch_datetime = DateTime.Now,
                    ch_oldvalue = beforeAsset.as_phid.ToString() ?? "",
                    ch_newvalue = as_phid.ToString() ?? "",
                    ch_field = "as_phid",
                    ch_recid = ID,
                    ch_usid = currentUser.ID
                });
            }
            return output;
        }

        /// <summary>
        /// If an asset is being saved, alerts may also need to be saved
        /// </summary>
        /// <param name="beforeObject">Database record of the current object</param>
        /// <returns>List of alerts based on before and after of this asset.</returns>
        public List<Alert> GetAlerts(Asset beforeObject, User currentUser)
        {
            List<Alert> output = new List<Alert>();
            if(beforeObject == null)
            {
                beforeObject = new Asset();
            }
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
                     ar_header = "You've been assigned to #" + as_id,
                     ar_content = Phase?.ph_name ?? "Status Unavailable" + " | " + as_displayname,
                     ar_recid = currentUser.ID,
                     ar_type = AlertType.AssetAssigned
                });
            }
            
            return output;
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
