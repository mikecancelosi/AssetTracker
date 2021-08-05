using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Strategies
{
    public interface IDeleteStrategy<T> where T : class
    {
        void Delete(GenericUnitOfWork uow, T item);
    }
}
