using DataAccessLayer;
using DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace AssetTracker.ViewModels.Services
{
    public class SecRoleValidator : IModelValidator<SecRole>
    {
        public bool IsValid(GenericUnitOfWork uow, SecRole item, out List<Violation> violations)
        {
            violations = new List<Violation>();

            if (item.ro_name == "" || item.ro_name == null)
            {
                violations.Add(new Violation("You need to set the role name!", "ro_name"));
            }

            return violations.Count() == 0;
        }
    }
}
