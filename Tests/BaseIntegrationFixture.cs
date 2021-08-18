using DataAccessLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Tests
{
    internal class BaseIntegrationFixture
    {
        protected TrackerContext Context => SetUpFixtures.Context;

        protected BaseIntegrationFixture()
        {
        }
       
    }
}
