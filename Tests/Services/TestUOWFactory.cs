using DataAccessLayer.Services;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class TestUOWFactory : IUnitOfWorkFactory
    {
        private IRepository<Asset> assetRepo;
        private IRepository<Discussion> discussionRepo;
        private IRepository<Change> changeRepo;
        private IRepository<Alert> alertRepo;
        private IRepository<Metadata> tagRepo;
        private IRepository<User> userRepo;
        private IRepository<Phase> phaseRepo;
        private IRepository<AssetCategory> assetCategoryRepo;

        public TestUOWFactory(IRepository<Asset> assetRepository = null,
                             IRepository<Discussion> discussionRepository = null,
                             IRepository<Change> changeRepository = null,
                             IRepository<Alert> alertRepository = null,
                             IRepository<Metadata> tagRepository = null,
                             IRepository<User> userRepository = null,
                             IRepository<Phase> phaseRepository = null,
                             IRepository<AssetCategory> assetCategoryRepository = null)

        {
            assetRepo = assetRepository ?? new GenericTestRepository<Asset>(new List<Asset>());
            discussionRepo = discussionRepository ?? new GenericTestRepository<Discussion>(new List<Discussion>());
            changeRepo = changeRepository ?? new GenericTestRepository<Change>(new List<Change>());
            alertRepo = alertRepository ?? new GenericTestRepository<Alert>(new List<Alert>());
            tagRepo = tagRepository ?? new GenericTestRepository<Metadata>(new List<Metadata>());
            userRepo = userRepository ?? new GenericTestRepository<User>(new List<User>());
            phaseRepo = phaseRepository ?? new GenericTestRepository<Phase>(new List<Phase>());
            assetCategoryRepo = assetCategoryRepository ?? new GenericTestRepository<AssetCategory>(new List<AssetCategory>());
        }

        public IUnitOfWork BuildUOW()
        {
            return new TestUnitOfWork(assetRepo,
                                      discussionRepo,
                                      changeRepo,
                                      alertRepo,
                                      tagRepo,
                                      userRepo,
                                      phaseRepo,
                                      assetCategoryRepo);
        }
    }
}
