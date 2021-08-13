using DomainModel;

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
