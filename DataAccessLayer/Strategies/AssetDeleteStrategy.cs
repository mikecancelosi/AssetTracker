using DataAccessLayer.Services;
using DomainModel;

namespace DataAccessLayer.Strategies
{
    public class AssetDeleteStrategy : IDeleteStrategy<Asset>
    {
        public void Delete(IUnitOfWork uow, Asset item)
        {
            IRepository<AssetLink> linkRepo = uow.GetRepository<AssetLink>();
            IRepository<Asset> assetRepo = uow.GetRepository<Asset>();

            if(item.AssetLink != null)
            {
                linkRepo.Delete(linkRepo);
            }

            if(item.as_usid > 0)
            {
                item.as_usid = 0;
            }

            assetRepo.Delete(item);
        }
    }
}
