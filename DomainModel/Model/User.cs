using System.Collections.Generic;
using System.Linq;

namespace DomainModel
{
    /// <summary>
    /// Users are set in project settings > Users
    /// Each separate employee should be given their own 
    /// separate user account. This allows for proper security
    /// and logging
    /// </summary>
    public partial class User : DatabaseBackedObject
    {
        public override int ID
        {
            get => us_id;
            set => us_id = value;
        }

        public override string Name
        {
            get => us_displayname;
            set => us_displayname = value;
        }

        public override bool IsValid(out List<Violation> violations)
        {
            violations = new List<Violation>();
            base.IsValid(out violations);
            return violations.Count() == 0;
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
