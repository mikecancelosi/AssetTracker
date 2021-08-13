using DataAccessLayer;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModels.Services
{
    public class AssetValidator : IModelValidator<Asset>
    {
        public bool IsValid(GenericUnitOfWork uow, Asset item, out List<Violation> violations)
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
