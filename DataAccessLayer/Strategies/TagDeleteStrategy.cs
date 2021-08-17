using DataAccessLayer.Services;
using DomainModel;

namespace DataAccessLayer.Strategies
{
    public class TagDeleteStrategy : IDeleteStrategy<Metadata>
    {
        public void Delete(IUnitOfWork uow, Metadata item)
        {
            IRepository<Metadata> tagRepo = uow.GetRepository<Metadata>();

            item.Asset?.Metadata.Remove(item);

            tagRepo.Delete(item);
        }
    }
}
