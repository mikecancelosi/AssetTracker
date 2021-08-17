using DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork BuildUOW()
        {
            return new GenericUnitOfWork(new DataAccessLayer.TrackerContext());
        }
    }
}
