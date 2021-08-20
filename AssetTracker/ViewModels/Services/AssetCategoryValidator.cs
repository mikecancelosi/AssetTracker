using DataAccessLayer;
using DataAccessLayer.Services;
using DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace Quipu.ViewModels.Services
{
    public class AssetCategoryValidator : IModelValidator<AssetCategory>
    {
        public bool IsValid(IUnitOfWork uow, AssetCategory item, out List<Violation> violations)
        {
            violations = new List<Violation>();

            if (item.ca_name == "" || item.ca_name == null)
            {
                violations.Add(new Violation("You need to set a name!", "ca_name"));
            }

            if (item.Phases.Count <= 0)
            {
                violations.Add(new Violation("A category needs to be assigned phases", "Phases"));
            }
            else
            {

                if (item.Phases.Any(x => x.ph_name == "" || x.ph_name == null))
                {
                    violations.Add(new Violation("One or more of your categories has an empty name", "ph_name"));
                }
            }

            return violations.Count() == 0;
        }
    }
}
