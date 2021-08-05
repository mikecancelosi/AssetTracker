using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Strategies
{
    public class AlertDeleteStrategy : IDeleteStrategy<Alert>
    {
        public void Delete(GenericUnitOfWork uow, Alert item)
        {
            GenericRepository<Alert> alertRepo = uow.GetRepository<Alert>();

            alertRepo.Delete(item);
        }
    }
}
