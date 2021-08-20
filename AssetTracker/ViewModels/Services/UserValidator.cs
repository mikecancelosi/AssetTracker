using DataAccessLayer;
using DataAccessLayer.Services;
using DomainModel;
using System.Collections.Generic;
using System.Linq;

namespace Quipu.ViewModels.Services
{
    public class UserValidator : IModelValidator<User>
    {
        public bool IsValid(IUnitOfWork uow, User item, out List<Violation> violations)
        {
            violations = new List<Violation>();

            if (item.us_fname == "" || item.us_fname == null)
            {
                violations.Add(new Violation("You need to set this user's first name!", "us_fname"));
            }

            if (item.us_lname == "" || item.us_lname == null)
            {
                violations.Add(new Violation("You need to set this user's last name!", "us_lname"));
            }

            if (item.us_displayname == "" || item.us_displayname == null)
            {
                violations.Add(new Violation("You need to set this user's displayname!", "us_displayname"));
            }

            if (item.us_email == "" || item.us_email == null)
            {
                violations.Add(new Violation("You need to set this user's email!", "us_email"));
            }
            else
            {
                IRepository<User> userRepo = uow.GetRepository<User>();
                User match = (from u in userRepo.Get()
                              where u.us_email == item.us_email
                              && u.us_id != item.ID
                              select u).FirstOrDefault();
                if(match != null)
                {
                    violations.Add(new Violation("This email already exists!", "us_email"));
                }

            }

            if (item.us_password == "" || item.us_password == null)
            {
                violations.Add(new Violation("You need to set this user's password!", "us_password"));
            }
           

            if (item.us_roid <= 0)
            {
                violations.Add(new Violation("You need to set this user's role!", "us_roid"));
            }

            return violations.Count == 0;
        }
    }
}
