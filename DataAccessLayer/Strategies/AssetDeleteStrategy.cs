using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Strategies
{
    public class AssetDeleteStrategy : IDeleteStrategy<Asset>
    {
        public void Delete(GenericUnitOfWork uow, Asset item)
        {
            GenericRepository<AssetLink> linkRepo = uow.GetRepository<AssetLink>();
            GenericRepository<Asset> assetRepo = uow.GetRepository<Asset>();

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
