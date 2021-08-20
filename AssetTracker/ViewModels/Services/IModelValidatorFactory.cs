
using DomainModel;

namespace Quipu.ViewModels.Services
{
    public interface IModelValidatorFactory
    {
        IModelValidator<Asset> BuildAssetValidator();
        IModelValidator<User> BuildUserValidator();
        IModelValidator<AssetCategory> BuildCategoryValidator();
        IModelValidator<SecRole> BuildRoleValidator();
    }
}
