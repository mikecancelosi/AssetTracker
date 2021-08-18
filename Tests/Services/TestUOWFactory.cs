using DataAccessLayer;
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
        private TrackerContext context;

        public TestUOWFactory(TrackerContext contextInstance)
        {
            context = contextInstance;
        }

        public IUnitOfWork BuildUOW()
        {
            return new GenericUnitOfWork(context);
        }
    }
}
