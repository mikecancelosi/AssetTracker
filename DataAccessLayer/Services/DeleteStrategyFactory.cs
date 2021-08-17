using DataAccessLayer.Strategies;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class DeleteStrategyFactory : IDeleteStrategyFactory
    {
        public IDeleteStrategy<Alert> BuildAlertStrategy()
        {
            return new AlertDeleteStrategy();
        }

        public IDeleteStrategy<Asset> BuildAssetStrategy()
        {
            return new AssetDeleteStrategy();
        }

        public IDeleteStrategy<AssetCategory> BuildCategoryStrategy()
        {
            return new CategoryDeleteStrategy();
        }

        public IDeleteStrategy<Metadata> BuildMetadataStrategy()
        {
            return new TagDeleteStrategy();
        }

        public IDeleteStrategy<SecRole> BuildRoleStrategy()
        {
            return new SecRoleDeleteStrategy();
        }

        public IDeleteStrategy<User> BuildUserStrategy()
        {
            return new UserDeleteStrategy();
        }
    }
}
