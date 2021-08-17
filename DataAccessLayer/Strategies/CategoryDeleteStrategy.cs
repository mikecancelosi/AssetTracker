using DataAccessLayer.Services;
using DomainModel;
using System.Linq;

namespace DataAccessLayer.Strategies
{
    public class CategoryDeleteStrategy : IDeleteStrategy<AssetCategory>
    {
        public void Delete(IUnitOfWork uow, AssetCategory item)
        {
            IRepository<AssetCategory> catRepo = uow.GetRepository<AssetCategory>();
            IRepository<Phase> phaseRepo = uow.GetRepository<Phase>();

            foreach(var asset in item.AssetsInCategory.ToList())
            {
                asset.as_caid = null;
            }

            foreach(var phase in item.Phases.ToList())
            {
                phaseRepo.Delete(phase);
            }

            catRepo.Delete(item);
        }
    }
}
