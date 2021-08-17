using DataAccessLayer;
using DataAccessLayer.Services;
using DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace AssetTracker.ViewModels.Services
{
    public class AssetValidator : IModelValidator<Asset>
    {
        public bool IsValid(IUnitOfWork uow, Asset item, out List<Violation> violations)
        {
            violations = new List<Violation>();

            if (item.as_description == "" || item.as_description == null)
            {
                violations.Add(new Violation("You need to add a description!", "as_description"));
            }

            if (item.as_displayname == "" || item.as_description == null)
            {
                violations.Add(new Violation("You need to set the name!", "as_displayname"));
            }

            return violations.Count() == 0;
        }
    }
}
