using DataAccessLayer.Strategies;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public interface IDeleteStrategyFactory
    {
        IDeleteStrategy<Asset> BuildAssetStrategy();
        IDeleteStrategy<User> BuildUserStrategy();
        IDeleteStrategy<SecRole> BuildRoleStrategy();
        IDeleteStrategy<AssetCategory> BuildCategoryStrategy();
        IDeleteStrategy<Alert> BuildAlertStrategy();
        IDeleteStrategy<Metadata> BuildMetadataStrategy();
    }
}
