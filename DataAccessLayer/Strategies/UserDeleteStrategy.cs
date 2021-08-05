using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Strategies
{
    public class UserDeleteStrategy : IDeleteStrategy<User>
    {
        public void Delete(GenericUnitOfWork uow, User item)
        {
            throw new NotImplementedException();
        }
    }
}
