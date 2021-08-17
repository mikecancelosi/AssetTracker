using AssetTracker.Services;
using AssetTracker.ViewModels;
using AssetTracker.ViewModels.Services;
using DataAccessLayer.Services;
using DataAccessLayer.Strategies;
using DomainModel;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Services;

namespace Tests
{
    public class AssetTests
    {
        
        private INavigationCoordinator navigationCoordinator;
        private IDeleteStrategyFactory deleteStrategyFactory;
        private ModelValidatorFactory modelValidatorFactory;

        [SetUp]
        public void Setup()
        {
            deleteStrategyFactory = new DeleteStrategyFactory();
            modelValidatorFactory = new ModelValidatorFactory();
            navigationCoordinator = new Mock<INavigationCoordinator>().Object;
        }

        [Test]
        public void CreateAsset()
        {
            //Arrange
            var uowFactory = new TestUOWFactory();
            IViewModelFactory viewModelFactory = new ViewModelFactory(deleteStrategyFactory, modelValidatorFactory, uowFactory);

            //Act
            var assetDetailViewModel = viewModelFactory.CreateAssetDetailViewModel(navigationCoordinator);

            //Assert
            Assert.AreEqual(0,assetDetailViewModel.myAsset.ID);

        }
        
        [Test]
        public void SetAsset()
        {
            //Arrange
            string expectedName = "Test object";
            Asset newAsset = new Asset()
            {
                as_description = "Object used for testing",
                as_displayname = expectedName
            };
            List<Asset> assetSource = new List<Asset>() { newAsset };
            GenericTestRepository<Asset> assetRepo = new GenericTestRepository<Asset>(assetSource);
            var uowFactory = new TestUOWFactory(assetRepo);
            IViewModelFactory viewModelFactory = new ViewModelFactory(deleteStrategyFactory, modelValidatorFactory, uowFactory);

            //Act
            var assetDetailViewModel = viewModelFactory.CreateAssetDetailViewModel(navigationCoordinator);
            assetDetailViewModel.SetAsset(newAsset);

            //Assert
            Assert.AreNotEqual("",assetDetailViewModel.myAsset.as_description);
            Assert.AreEqual(expectedName,assetDetailViewModel.myAsset.as_displayname);
        }

        [Test]
        public void AddTag()
        {
            //Arrange
            string expectedName = "Test object";
            Asset newAsset = new Asset()
            {
                as_description = "Object used for testing",
                as_displayname = expectedName
            };
            List<Asset> assetSource = new List<Asset>() { newAsset };
            GenericTestRepository<Asset> assetRepo = new GenericTestRepository<Asset>(assetSource);
            var uowFactory = new TestUOWFactory(assetRepo);
            IViewModelFactory viewModelFactory = new ViewModelFactory(deleteStrategyFactory, modelValidatorFactory, uowFactory);

            //Act
            var assetDetailViewModel = viewModelFactory.CreateAssetDetailViewModel(navigationCoordinator);
            assetDetailViewModel.SetAsset(newAsset);
            assetDetailViewModel.AddTag("TestTag");

            //Assert
            Assert.AreEqual(1,assetDetailViewModel.Tags.Count);
        }

        [Test]
        public void DeleteTag()
        {
            //Arrange
            string expectedName = "Test object";
            Asset newAsset = new Asset()
            {
                as_description = "Object used for testing",
                as_displayname = expectedName
            };
            List<Asset> assetSource = new List<Asset>() { newAsset };
            GenericTestRepository<Asset> assetRepo = new GenericTestRepository<Asset>(assetSource);
            var uowFactory = new TestUOWFactory(assetRepo);
            IViewModelFactory viewModelFactory = new ViewModelFactory(deleteStrategyFactory, modelValidatorFactory, uowFactory);

            //Act
            var assetDetailViewModel = viewModelFactory.CreateAssetDetailViewModel(navigationCoordinator);
            assetDetailViewModel.SetAsset(newAsset);
            assetDetailViewModel.AddTag("TestTag");
            assetDetailViewModel.DeleteTag("TestTag");

            //Assert
            Assert.AreEqual(0,assetDetailViewModel.Tags.Count);
        }
    }
}
