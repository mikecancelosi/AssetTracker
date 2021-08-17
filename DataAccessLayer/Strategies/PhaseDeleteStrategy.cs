using DataAccessLayer.Services;
using DomainModel;
using System.Linq;

namespace DataAccessLayer.Strategies
{
    public class PhaseDeleteStrategy : IDeleteStrategy<Phase>
    {
        public void Delete(IUnitOfWork uow, Phase item)
        {
            IRepository<Phase> phaseRepo = uow.GetRepository<Phase>();

            foreach(var asset in item.AssetsInPhase.ToList())
            {
                asset.as_phid = null;
            }

            phaseRepo.Delete(item);
        }
    }
}
