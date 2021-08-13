namespace DataAccessLayer.Strategies
{
    public interface IDeleteStrategy<T> where T : class
    {
        void Delete(GenericUnitOfWork uow, T item);
    }
}
