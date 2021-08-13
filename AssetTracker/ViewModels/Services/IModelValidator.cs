using DataAccessLayer;
using DomainModel;
using System.Collections.Generic;

namespace AssetTracker.ViewModels.Services
{
    /// <summary>
    /// Validators remove validation responsbility from the model and
    /// allow for access to the database for extended validation
    /// </summary>
    /// <typeparam name="T">Type of DBO to validate</typeparam>
    public interface IModelValidator<T> where T : class
    {
        /// <summary>
        /// Determines whether or not the given dbo is valid
        /// </summary>
        /// <param name="uow">Unit of work to use to access the db</param>
        /// <param name="item">DBO to validate</param>
        /// <param name="violations">Any violations that were found</param>
        /// <returns>Whether the dbo is valid ( if any violations were found this is false )</returns>
        bool IsValid(GenericUnitOfWork uow, T item, out List<Violation> violations);
    }
}
