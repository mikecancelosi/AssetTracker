using System.Collections.Generic;
using System.Linq;

namespace DomainModel
{
    /// <summary>
    /// Assets are given a category they belong to. This helps
    /// organization to enable better filtering. Phases are also
    /// connected to Categories as each category has a different
    /// set of phases.
    /// </summary>
    public partial class AssetCategory : DatabaseBackedObject
    {

        public override int ID
        {
            get => ca_id;
            set => ca_id = value;
        }

        public override string Name
        {
            get => ca_name;
            set => ca_name = value;
        }

        public override DatabaseBackedObject Clone()
        {
            return new AssetCategory()
            {
                ca_name = ca_name + "(Clone)"
            };
        }

        public override bool IsValid(out List<Violation> violations)
        {         
            base.IsValid(out violations);

            if(ca_name == "" || ca_name == null)
            {
                violations.Add(new Violation("You need to set a name!", "ca_name"));
            }

            if (Phases.Count <= 0)
            {
                violations.Add(new Violation("A category needs to be assigned phases", "Phases"));
            }
            else
            {

                if (Phases.Any(x=>x.ph_name == "" || x.ph_name == null))
                {
                    violations.Add(new Violation("One or more of your categories has an empty name", "ph_name"));
                }
            }

            return violations.Count() == 0;
        }
    }
}
