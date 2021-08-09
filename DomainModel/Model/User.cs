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
            base.IsValid(out violations);

            if(us_fname == "" || us_fname == null)
            {
                violations.Add(new Violation("You need to set this user's first name!", "us_fname"));
            }

            if (us_lname == "" || us_lname == null)
            {
                violations.Add(new Violation("You need to set this user's last name!", "us_lname"));
            }

            if (us_displayname == "" || us_displayname == null)
            {
                violations.Add(new Violation("You need to set this user's displayname!", "us_displayname"));
            }

            if (us_email == "" || us_email == null)
            {
                violations.Add(new Violation("You need to set this user's email!", "us_email"));
            }

            if (us_password == "" || us_password == null)
            {
                violations.Add(new Violation("You need to set this user's password!", "us_password"));
            }

            if(us_roid <= 0)
            {
                violations.Add(new Violation("You need to set this user's role!", "us_roid"));
            }

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
