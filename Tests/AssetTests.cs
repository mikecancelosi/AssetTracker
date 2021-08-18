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
using System.Transactions;
using Tests.Services;

namespace Tests
{
    internal sealed class AssetTests : BaseIntegrationFixture
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
            var uowFactory = new TestUOWFactory(Context);
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
            Asset testAsset = new Asset()
            {
                as_displayname = "Test Object",
                as_description = "Object for testing"
            };
            Context.Assets.Add(testAsset);
            string expectedName = testAsset.as_displayname;
            var uowFactory = new TestUOWFactory(Context);
            IViewModelFactory viewModelFactory = new ViewModelFactory(deleteStrategyFactory, modelValidatorFactory, uowFactory);

            //Act
            
            var assetDetailViewModel = viewModelFactory.CreateAssetDetailViewModel(navigationCoordinator);
            assetDetailViewModel.SetAsset(testAsset);

            //Assert
            Assert.AreNotEqual("",assetDetailViewModel.myAsset.as_description);
            Assert.AreEqual(expectedName,assetDetailViewModel.myAsset.as_displayname);
        }

        [Test]
        public void AddTag()
        {
            //Arrange
            Asset testAsset = Context.Assets.FirstOrDefault();           
            var uowFactory = new TestUOWFactory(Context);
            IViewModelFactory viewModelFactory = new ViewModelFactory(deleteStrategyFactory, modelValidatorFactory, uowFactory);

            //Act
            var assetDetailViewModel = viewModelFactory.CreateAssetDetailViewModel(navigationCoordinator);
            assetDetailViewModel.SetAsset(testAsset);
            assetDetailViewModel.AddTag("TestTag");

            //Assert
            Assert.AreEqual(1,assetDetailViewModel.Tags.Count);
        }

        [Test]
        public void DeleteTag()
        {
            //Arrange
            Asset testAsset = Context.Assets.FirstOrDefault();
            var uowFactory = new TestUOWFactory(Context);
            IViewModelFactory viewModelFactory = new ViewModelFactory(deleteStrategyFactory, modelValidatorFactory, uowFactory);

            //Act
            var assetDetailViewModel = viewModelFactory.CreateAssetDetailViewModel(navigationCoordinator);
            assetDetailViewModel.SetAsset(testAsset);
            assetDetailViewModel.AddTag("TestTag");
            assetDetailViewModel.DeleteTag("TestTag");

            //Assert
            Assert.AreEqual(0,assetDetailViewModel.Tags.Count);
        }
    }
}
