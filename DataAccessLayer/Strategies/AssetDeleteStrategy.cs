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
            throw new NotImplementedException();
        }
    }
}
