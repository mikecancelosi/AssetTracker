using DataAccessLayer.Services;

namespace DataAccessLayer.Strategies
{
    public interface IDeleteStrategy<T> where T : class
    {
        void Delete(IUnitOfWork uow, T item);
    }
}
