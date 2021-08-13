using DataAccessLayer;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModels.Services
{
    public interface IModelValidator<T> where T : class
    {
        bool IsValid(GenericUnitOfWork uow, T item, out List<Violation> violations);
    }
}
