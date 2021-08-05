using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Strategies
{
    public class PhaseDeleteStrategy : IDeleteStrategy<Phase>
    {
        public void Delete(GenericUnitOfWork uow, Phase item)
        {
            GenericRepository<Phase> phaseRepo = uow.GetRepository<Phase>();

            foreach(var asset in item.AssetsInPhase.ToList())
            {
                asset.as_phid = null;
            }

            phaseRepo.Delete(item);
        }
    }
}
