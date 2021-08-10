using DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel
{
    /// <summary>
    /// Discussions are displayed under an asset detail. There will be 
    /// one parent discussion and many child discussions. If a child 
    /// discussion is 'replied' to, the new reply will still go under
    /// the parent.
    /// 
    /// Discussion replies will alert other people who have written in 
    /// the parent discussion
    /// 
    /// Discussion starters will alert whoever is assigned to the asset.
    /// </summary>
    public partial class Discussion : DatabaseBackedObject
    {
        public override int ID
        {
            get => di_id;
            set => di_id = value;
        }

        public override string Name
        {
            get => di_contents;
            set => di_contents = value;
        }

        public List<Alert> GetAlerts(List<Discussion> relatedDiscussions)
        {
            List<Alert> output = new List<Alert>();

            List<User> usersToAlert = relatedDiscussions.Select(x => x.User).Distinct().ToList();            
            foreach (User u in usersToAlert)
            {
                Alert newAlert = new Alert()
                {
                    ar_usid = u.ID,
                    ar_projectwide = false,
                    ar_asid = this.di_asid,
                    ar_date = DateTime.Now,
                    ar_type = AlertType.DiscussionReply,
                    ar_header = User.us_displayname + " continued the conversation on #" + Asset.as_id,
                    ar_content = di_contents.Substring(0, Math.Min(di_contents.Length, 47)) + "..."
                };
                output.Add(newAlert);
            }

            return output;

        }
    }
}
