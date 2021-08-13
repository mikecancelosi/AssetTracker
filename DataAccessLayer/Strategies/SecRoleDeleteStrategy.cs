using DomainModel;
using System.Linq;

namespace DataAccessLayer.Strategies
{
    public class SecRoleDeleteStrategy : IDeleteStrategy<SecRole>
    {
        public void Delete(GenericUnitOfWork uow, SecRole item)
        {
            var roleRepo = uow.GetRepository<SecRole>();
            var roleOverrideRepo = uow.GetRepository<SecPermission2>();

            foreach (User user in item.Users)
            {
                //TODO: Show screen to show current users for reassignment. -- ASP
                user.us_roid = (from r in roleRepo.Get()
                                where r.ro_id != item.ro_id
                                select r).FirstOrDefault().ID;
            }

            foreach (SecPermission3 roleOverride in item.SecPermission3.ToList())
            {
                roleOverrideRepo.Delete(roleOverride);
            }

            roleRepo.Delete(item);
        }
    }
}
