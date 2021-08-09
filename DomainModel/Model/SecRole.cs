using System.Collections.Generic;
using System.Linq;

namespace DomainModel
{
    /// <summary>
    /// Each user is assigned a role. This gives the user default permissions
    /// (set in Project Settings > Roles > Role Edit).
    /// </summary>
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
            base.IsValid(out violations);

            if(ro_name == "" || ro_name == null)
            {
                violations.Add(new Violation("You need to set the role name!", "ro_name"));
            }

            return violations.Count() == 0;
        }      
    }
}
