using DataAccessLayer;
using Effort.Provider;
using NUnit.Framework;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;

namespace Tests
{
    [SetUpFixture]
    internal sealed class SetUpFixtures
    {
        internal static TrackerContext Context;
        private DbConnection connection;

        [OneTimeSetUp]
        public void Initialize()
        {
            EffortProviderConfiguration.RegisterProvider();
            connection = Effort.EntityConnectionFactory.CreatePersistent("name=TrackerContext");
            Context = new TrackerContext(connection);
            
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            Context.Dispose();
        }
    }
}
