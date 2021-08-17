using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModels.Services
{
    public class ModelValidatorFactory : IModelValidatorFactory
    {
        public IModelValidator<Asset> BuildAssetValidator()
        {
            return new AssetValidator();
        }

        public IModelValidator<AssetCategory> BuildCategoryValidator()
        {
            return new AssetCategoryValidator();
        }

        public IModelValidator<SecRole> BuildRoleValidator()
        {
            return new SecRoleValidator();
        }

        public IModelValidator<User> BuildUserValidator()
        {
            return new UserValidator();
        }
    }
}
