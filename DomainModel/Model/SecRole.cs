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
            violations = new List<Violation>();
            base.IsValid(out violations);
            return violations.Count() == 0;
        }      
    }
}
