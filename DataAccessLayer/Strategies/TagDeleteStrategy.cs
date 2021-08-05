using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Strategies
{
    public class TagDeleteStrategy : IDeleteStrategy<Metadata>
    {
        public void Delete(GenericUnitOfWork uow, Metadata item)
        {
            GenericRepository<Metadata> tagRepo = uow.GetRepository<Metadata>();

            item.Asset.Metadata.Remove(item);

            tagRepo.Delete(item);
        }
    }
}
