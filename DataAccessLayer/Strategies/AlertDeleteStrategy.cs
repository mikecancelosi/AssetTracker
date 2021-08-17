using DataAccessLayer.Services;
using DomainModel;

namespace DataAccessLayer.Strategies
{
    public class AlertDeleteStrategy : IDeleteStrategy<Alert>
    {
        public void Delete(IUnitOfWork uow, Alert item)
        {
            IRepository<Alert> alertRepo = uow.GetRepository<Alert>();

            alertRepo.Delete(item);
        }
    }
}
