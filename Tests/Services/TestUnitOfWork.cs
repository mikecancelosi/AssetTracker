using DataAccessLayer.Services;
using DomainModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class TestUnitOfWork : IUnitOfWork
    {
        public bool HasChanges => throw new NotImplementedException();
        private IRepository<Asset> assetRepo;
        private IRepository<Discussion> discussionRepo;
        private IRepository<Change> changeRepo;
        private IRepository<Alert> alertRepo;
        private IRepository<Metadata> tagRepo;
        private IRepository<User> userRepo;
        private IRepository<Phase> phaseRepo;
        private IRepository<AssetCategory> assetCategoryRepo;

        public TestUnitOfWork(IRepository<Asset> assetRepository =null,
                             IRepository<Discussion> discussionRepository = null,
                             IRepository<Change> changeRepository = null,
                             IRepository<Alert> alertRepository = null,
                             IRepository<Metadata> tagRepository = null,
                             IRepository<User> userRepository = null,
                             IRepository<Phase> phaseRepository = null,
                             IRepository<AssetCategory> assetCategoryRepository = null)           

        {
            assetRepo = assetRepository;
            discussionRepo = discussionRepository;
            changeRepo = changeRepository;
            alertRepo = alertRepository;
            tagRepo = tagRepository;
            userRepo = userRepository;
            phaseRepo = phaseRepository;
            assetCategoryRepo = assetCategoryRepository;
        }
        

        public void Commit()
        {
            Console.WriteLine("Fake commit.");
        }

        public void Dispose()
        {
            Console.WriteLine("Fake Dispose.");
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            Type tType = typeof(T);

            switch(tType.Name)
            {
                case "Asset":
                    return (IRepository<T>)assetRepo;
                case "Discussion":
                    return (IRepository<T>)discussionRepo;
                case "Change":
                    return (IRepository<T>)changeRepo;
                case "Alert":
                    return (IRepository<T>)alertRepo;
                case "Metadata":
                    return (IRepository<T>)tagRepo;
                case "User":
                    return (IRepository<T>)userRepo;
                case "Phase":
                    return (IRepository<T>)phaseRepo;
                case "AssetCategory":
                    return (IRepository<T>)assetCategoryRepo;
                default:
                    throw new NotImplementedException();
            }

        }

        public void Rollback()
        {
            Console.WriteLine("Fake Rollback.");
        }
    }
}
