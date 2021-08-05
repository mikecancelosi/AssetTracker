using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Strategies
{
    public class CategoryDeleteStrategy : IDeleteStrategy<AssetCategory>
    {
        public void Delete(GenericUnitOfWork uow, AssetCategory item)
        {
            GenericRepository<AssetCategory> catRepo = uow.GetRepository<AssetCategory>();
            GenericRepository<Phase> phaseRepo = uow.GetRepository<Phase>();

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
